using UnityEngine;

public class BuildFarmButton : MonoBehaviour
{
    [SerializeField] private int farmLevel;

    public void OnBuild() {
        PopupManager.Instance.BuildFarm(farmLevel);
    }
}
