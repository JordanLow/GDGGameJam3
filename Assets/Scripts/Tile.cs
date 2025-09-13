using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{

    private enum TileState {
        Empty = 0,
        Add,
        Free,
        Used
    }

    private enum Fruit {
        Strawberry = 0,
        Pineapple,
        Watermelon,
        Lemon,
        Apple,
        Pear,
        Grape,
        Banana,
        Kiwi,
        Mango
    }

    private const int fruitCount = 10;

    [SerializeField] public GameObject addStateTexture;
    [SerializeField] public GameObject existStateTexture;
/*
    0: [SerializeField] public GameObject strawberryFarmPrefab;
    1: [SerializeField] public GameObject pineappleFarmPrefab;
    2: [SerializeField] public GameObject lemonFarmPrefab;
    3: [SerializeField] public GameObject watermelonFarmPrefab;
    4: [SerializeField] public GameObject appleFarmPrefab;
    5: [SerializeField] public GameObject pearFarmPrefab;
    6: [SerializeField] public GameObject grapeFarmPrefab;
    7: [SerializeField] public GameObject kiwiFarmPrefab;
    8: [SerializeField] public GameObject bananaFarmPrefab;
    9: [SerializeField] public GameObject mangoFarmPrefab;
*/

    [SerializeField] public List<GameObject> farmPrefabs;

    [SerializeField] public GameObject menuPrefab;

    private int[,] farmSizes = new int[Tile.fruitCount, 2] {{1, 1}, {1, 2}, {2, 2}, {2, 2}, {2, 3}, {3, 3}, {2, 4}, {2, 3}, {3, 4}, {3, 5}};

    private IslandManager islandManager;

    // private TileState state = TileState.Empty;
    // TEST
    // private TileState state = TileState.Used;
    private TileState state = TileState.Free;
    // private TileState state = TileState.Add;
    // ENDTEST
    private int xIndexPos;
    private int yIndexPos;
    private float xPos;
    private float yPos;

    private Farm currentFarm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.xPos = transform.position.x;
        this.yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static Farm spawnFarmPrefab(GameObject prefab, Vector3 position) {
        Farm instance = Instantiate(prefab, position, Quaternion.identity).GetComponent<Farm>();
        return instance;
    }

    public void setIslandManager(IslandManager islandManager) {
        this.islandManager = islandManager;
    }

    public void setFarm(Farm farm) {
        this.currentFarm = farm;
    }

    public void setIndexPosition(int x, int y) {
        this.xIndexPos = x;
        this.yIndexPos = y;
    }

    public void setEmpty() {
        this.state = TileState.Empty;
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(false);
    }

    public void setAdd() {
        this.state = TileState.Add;
        addStateTexture.SetActive(true);
        existStateTexture.SetActive(false);

    }

    public void setFree() {
        this.state = TileState.Free;
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(true);
    }

    public void setUsed() {
        this.state = TileState.Used;
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(true);
    }

    public bool isFree() {
        return this.state == TileState.Free;
    }

    public bool isAdd() {
        return this.state == TileState.Add;
    }

    public bool tryAdd(int[] fruitCount) {
        return false;
    }

    public void openClickMenu() {
        PopupManager.Instance.ShowMenuForObject(gameObject, menuPrefab);
        if (this.state == TileState.Used) {
            PopupManager.Instance.EnableDemolishButton();
            PopupManager.Instance.EnableHarvestButton();
        }
        if (this.state == TileState.Free) {
            int idx = 1; // ResourceManager.level?
            PopupManager.Instance.EnableBuildButton(idx);
        } 
        if (this.state == TileState.Add) {
            PopupManager.Instance.EnableAddButton();
        }
    }

    public bool buildFarm(int idx) {
        int sizeX = farmSizes[(int) Fruit.Strawberry, 0];
        int sizeY = farmSizes[(int) Fruit.Strawberry, 1];
        if (islandManager.checkTilesFree(xIndexPos, yIndexPos, sizeX, sizeY)) {
            currentFarm = spawnFarmPrefab(farmPrefabs[idx], new Vector3(xPos, yPos, 0));
            currentFarm.setIslandManager(islandManager);
            currentFarm.setUsedTiles(xIndexPos, yIndexPos, sizeX, sizeY);
            return true;
        } else {
            return false;
        }

    }
}
