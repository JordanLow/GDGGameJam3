using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [SerializeField] private Canvas popupCanvas;
    [SerializeField] private GameObject harvestButton;
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject demolishButton;
    [SerializeField] private GameObject levelUpButton;

    [SerializeField] private List<GameObject> buildButtons;

    private GameObject activeMenu;
    private GameObject activeTile;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    public void ShowMenuForObject(GameObject obj, GameObject menuPrefab)
    {

        if (activeMenu != null) CloseMenu();

        activeMenu = Instantiate(menuPrefab, popupCanvas.transform);
        activeTile = obj;
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
            activeTile = null;

            Debug.Log("te");
        }
    }

    public void EnableAddButton()
    {
        Instantiate(addButton, this.activeMenu.transform);
    }

    public void EnableDemolishButton()
    {
        Instantiate(demolishButton, this.activeMenu.transform);
    }

    public void EnableHarvestButton()
    {
        Instantiate(harvestButton, this.activeMenu.transform);
    }

    public void EnableBuildButton(int idx) {
        for (int i = 0; i < idx; i++) {
            Instantiate(buildButtons[i], this.activeMenu.transform);
        }
    }

    public void EnableLevelUp() {
        Instantiate(levelUpButton, this.activeMenu.transform);
    }

    public void BuildFarm(int i) {
        this.activeTile.buildFarm(i);
    }

    public void Demolish() {
        this.activeTile.demolishFarmOnTile();
    }

    public void Add() {
        this.activeTile.buildTile();
    }

}
