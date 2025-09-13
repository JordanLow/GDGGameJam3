// TimeBasedPostProcess.cs
// Attach to a GameObject with a URP Volume (or enable Auto Find/Create).
// Requires UnityEngine.Rendering.Universal.

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
[DisallowMultipleComponent]
public class TimeBasedPostProcess : MonoBehaviour
{
    [Header("Volume Source")]
    [Tooltip("Global URP Volume to drive. Leave empty and enable Auto Find/Create to set up automatically.")]
    public Volume volume;

    [Tooltip("If true, tries to find a global Volume on this GameObject; if none exists, it creates one with a new VolumeProfile.")]
    public bool autoFindOrCreateGlobalVolume = true;

    [Header("Time Settings")]
    [Tooltip("Animate with an internal looping clock.")]
    public bool useLoopingClock = true;

    [Tooltip("Total seconds for one full cycle when using the looping clock.")]
    public float loopDurationSeconds = 120f;

    [Tooltip("Manual time of day in hours (0..24). Only used when not using the looping clock.")]
    [Range(0f, 24f)] public float timeOfDayHours = 12f;

    [Tooltip("When true, time value wraps 0..1; when false it clamps.")]
    public bool loopTime = true;

    [Header("Color Adjustments")]
    [Tooltip("EV exposure over time (x: 0..1 normalized day, y: EV).")]
    public AnimationCurve postExposureEV = AnimationCurve.Linear(0, 0, 1, 0);

    [Tooltip("Contrast over time (-100..100).")]
    public AnimationCurve contrast = AnimationCurve.Linear(0, 0, 1, 0);

    [Tooltip("Color filter over time.")]
    public Gradient colorFilter = new Gradient
    {
        colorKeys = new[]
        {
            new GradientColorKey(new Color(0.95f,0.95f,1f), 0f), // cool dawn
            new GradientColorKey(Color.white, 0.5f),              // noon neutral
            new GradientColorKey(new Color(1f,0.95f,0.85f), 1f)   // warm dusk
        }
    };

    [Header("White Balance")]
    [Tooltip("Temperature over time (-100..100). Negative is cooler, positive warmer.")]
    public AnimationCurve temperature = AnimationCurve.Linear(0, -10, 1, 10);

    [Header("Bloom")]
    [Tooltip("Bloom intensity over time (0..10).")]
    public AnimationCurve bloomIntensity = AnimationCurve.Linear(0, 0.2f, 1, 0.7f);

    [Tooltip("Bloom threshold over time.")]
    public AnimationCurve bloomThreshold = AnimationCurve.Linear(0, 1.1f, 1, 1.1f);

    [Header("Vignette")]
    [Tooltip("Vignette intensity over time (0..1).")]
    public AnimationCurve vignetteIntensity = AnimationCurve.Linear(0, 0.12f, 1, 0.2f);

    [Tooltip("Vignette smoothness over time (0..1).")]
    public AnimationCurve vignetteSmoothness = AnimationCurve.Linear(0, 0.3f, 1, 0.5f);

    // Cached components
    ColorAdjustments _colorAdj;
    WhiteBalance _whiteBalance;
    Bloom _bloom;
    Vignette _vignette;

    void OnEnable()
    {
        EnsureVolume();
        EnsureOverrides();
        ApplyNow();
    }

    void OnValidate()
    {
        if (!isActiveAndEnabled) return;
        EnsureVolume();
        EnsureOverrides();
        ApplyNow();
    }

    void Update()
    {
        if (!volume) return;
        ApplyNow();
    }

    void EnsureVolume()
    {
        if (volume == null && autoFindOrCreateGlobalVolume)
        {
            volume = GetComponent<Volume>();
            if (volume == null) volume = gameObject.AddComponent<Volume>();
        }

        if (volume != null)
        {
            volume.isGlobal = true;
            if (volume.profile == null)
            {
                // Use a local profile so edits don’t touch a shared asset by accident.
                volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();
#if UNITY_EDITOR
                volume.profile.name = $"{gameObject.name}_AutoProfile";
#endif
            }
        }
    }

    void EnsureOverrides()
    {
        if (!volume || volume.profile == null) return;

        // Add or grab components and enable overrideState
        _colorAdj = GetOrAdd<ColorAdjustments>(volume.profile);
        _colorAdj.postExposure.overrideState = true;
        _colorAdj.contrast.overrideState = true;
        _colorAdj.colorFilter.overrideState = true;

        _whiteBalance = GetOrAdd<WhiteBalance>(volume.profile);
        _whiteBalance.temperature.overrideState = true;

        _bloom = GetOrAdd<Bloom>(volume.profile);
        _bloom.intensity.overrideState = true;
        _bloom.threshold.overrideState = true;

        _vignette = GetOrAdd<Vignette>(volume.profile);
        _vignette.intensity.overrideState = true;
        _vignette.smoothness.overrideState = true;
    }

    static T GetOrAdd<T>(VolumeProfile profile) where T : VolumeComponent, new()
    {
        if (!profile.TryGet<T>(out var comp))
        {
            comp = profile.Add<T>(true);
        }
        return comp;
    }

    void ApplyNow()
    {
        float t = GetNormalizedTime01();

        if (_colorAdj != null)
        {
            _colorAdj.postExposure.value = postExposureEV.Evaluate(t);
            _colorAdj.contrast.value     = Mathf.Clamp(contrast.Evaluate(t), -100f, 100f);
            _colorAdj.colorFilter.value  = colorFilter.Evaluate(t);
        }

        if (_whiteBalance != null)
        {
            _whiteBalance.temperature.value = Mathf.Clamp(temperature.Evaluate(t), -100f, 100f);
        }

        if (_bloom != null)
        {
            _bloom.intensity.value = Mathf.Max(0f, bloomIntensity.Evaluate(t));
            _bloom.threshold.value = Mathf.Max(0f, bloomThreshold.Evaluate(t));
        }

        if (_vignette != null)
        {
            _vignette.intensity.value   = Mathf.Clamp01(vignetteIntensity.Evaluate(t));
            _vignette.smoothness.value  = Mathf.Clamp01(vignetteSmoothness.Evaluate(t));
        }
    }

    float GetNormalizedTime01()
    {
        if (useLoopingClock)
        {
            float dur = Mathf.Max(0.01f, loopDurationSeconds);
            float raw = Application.isPlaying ? (Time.time / dur) : (Time.realtimeSinceStartup / dur);
            return loopTime ? Mathf.Repeat(raw, 1f) : Mathf.Clamp01(raw);
        }
        else
        {
            return Mathf.Repeat(timeOfDayHours / 24f, 1f);
        }
    }

    // Optional helper: call from other scripts to sync external day/night systems.
    public void SetTimeOfDayHours(float hours)
    {
        useLoopingClock = false;
        timeOfDayHours = Mathf.Repeat(hours, 24f);
        ApplyNow();
    }
}
