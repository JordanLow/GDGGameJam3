using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [SerializeField] private Canvas popupCanvas;

    private GameObject activeMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    public void ShowMenuForObject(ClickableObject obj, GameObject menuPrefab)
    {

        if (activeMenu != null) Destroy(activeMenu);

        activeMenu = Instantiate(menuPrefab, popupCanvas.transform);

        Vector2 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
        activeMenu.GetComponent<RectTransform>().anchoredPosition = screenPos;
    }

    public void CloseMenu()
    {
        if (activeMenu != null)
        {
            Destroy(activeMenu);
            activeMenu = null;
        }
    }
}
