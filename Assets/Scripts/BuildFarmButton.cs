using UnityEngine;

public class BuildFarmButton : MonoBehaviour
{
    [SerializeField] private int farmLevel;

    public void OnBuild() {
        Debug.Log(farmLevel);
        // PopupManager.Instance.BuildFarm(farmLevel)
    }
}
