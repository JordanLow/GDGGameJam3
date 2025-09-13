using UnityEngine;

public class CloseButton : MonoBehaviour
{
    
    public void OnClose() {
        Debug.Log("closing");
        PopupManager.Instance.CloseMenu();
    }

}
