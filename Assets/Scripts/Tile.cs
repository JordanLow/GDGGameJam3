using UnityEngine;

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

    [SerializeField] public static GameObject addStateTexture;
    [SerializeField] public static GameObject existStateTexture;

    [SerializeField] public GameObject menuPrefab;

    [SerializeField] public static GameObject strawberryFarmPrefab;
    [SerializeField] public static GameObject pineappleFarmPrefab;
    [SerializeField] public static GameObject lemonFarmPrefab;
    [SerializeField] public static GameObject watermelonFarmPrefab;
    [SerializeField] public static GameObject appleFarmPrefab;
    [SerializeField] public static GameObject pearFarmPrefab;
    [SerializeField] public static GameObject grapeFarmPrefab;
    [SerializeField] public static GameObject kiwiFarmPrefab;
    [SerializeField] public static GameObject bananaFarmPrefab;
    [SerializeField] public static GameObject mangoFarmPrefab;

    private int[,] farmSizes = new int[Tile.fruitCount, 2] {{1, 1}, {1, 2}, {2, 2}, {2, 2}, {2, 3}, {3, 3}, {2, 4}, {2, 3}, {3, 4}, {3, 5}};

    private IslandManager islandManager;

    private TileState state = TileState.Empty;
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
        if (this.state == TileState.USED) {
            // Demolish
            // If Harvestable, Harvest
        }
        // Else if FREE: Build options
        // ELse if ADD: Add tile option
        PopupManager.Instance.ShowMenuForObject(gameObject, menuPrefab);
    }

    public bool buildStrawberryFarm() {
        int sizeX = farmSizes[(int) Fruit.Strawberry, 0];
        int sizeY = farmSizes[(int) Fruit.Strawberry, 1];
        if (islandManager.checkTilesFree(xIndexPos, yIndexPos, sizeX, sizeY)) {
            currentFarm = spawnFarmPrefab(strawberryFarmPrefab, new Vector3(xPos, yPos, 0));
            currentFarm.setIslandManager(islandManager);
            currentFarm.setUsedTiles(xIndexPos, yIndexPos, sizeX, sizeY);
            return true;
        } else {
            return false;
        }

    }
}
