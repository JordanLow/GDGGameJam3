using UnityEngine;

public class AddButton : MonoBehaviour
{
   
    public void OnAdd() {
        PopupManager.Instance.Add();
    }

}
