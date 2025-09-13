using UnityEngine;

public class HarvestButton : MonoBehaviour
{
    public void OnHarvest() {
        PopupManager.Instance.Harvest();
    }
}
