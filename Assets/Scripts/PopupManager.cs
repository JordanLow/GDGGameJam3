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

    public void ShowMenuForObject(GameObject obj, GameObject menuPrefab)
    {

        if (activeMenu != null) CloseMenu();

        activeMenu = Instantiate(menuPrefab, popupCanvas.transform);
        RectTransform popup = activeMenu.GetComponent<RectTransform>();

        // Convert world position to local canvas position
        Vector2 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            popupCanvas.transform as RectTransform,
            screenPos,
            null, // null if Screen Space - Overlay
            out localPoint
        );

        popup.anchoredPosition = localPoint;
        Debug.Log(popup.anchoredPosition);

        // Optionally scale popup based on object size
        float sizeFactor = obj.transform.localScale.magnitude * 10f;
        popup.sizeDelta = new Vector2(sizeFactor, sizeFactor);
    }

    public void CloseMenu()
    {
        if (activeMenu != null)
        {
            Destroy(activeMenu);
            activeMenu = null;

            Debug.Log("te");
        }
    }
}
