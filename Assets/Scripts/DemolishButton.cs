using UnityEngine;

public class DemolishButton : MonoBehaviour
{
   
    public void OnDemolish() {
        PopupManager.Instance.Demolish();
    }

}