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

    private const int fruitTypeCount = 10;

    [SerializeField] public GameObject addStateTexture;
    [SerializeField] public GameObject existStateTexture;

    [SerializeField] public List<GameObject> farmPrefabs;

    private int[,] farmSizes = new int[Tile.fruitTypeCount, 2] {{1, 1}, {1, 2}, {2, 2}, {2, 2}, {2, 3}, {3, 3}, {2, 4}, {2, 3}, {3, 4}, {3, 5}};

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

    public bool buildTile() {
        if (ResourceManager.hasEnoughFruitsToBuildTile(IslandManager.existingTileCount)) {
            this.setFree();
        }
        Debug.Log("Not enough resources");
        return false;
    }

    public void openClickMenu() {

    }

    public bool buildFarm(int index) {
        int sizeX = farmSizes[index, 0];
        int sizeY = farmSizes[index, 1];
        if (islandManager.checkTilesFree(xIndexPos, yIndexPos, sizeX, sizeY)) {
            currentFarm = spawnFarmPrefab(farmPrefabs[index], new Vector3(xPos, yPos, 0));
            currentFarm.setIslandManager(islandManager);
            currentFarm.setUsedTiles(xIndexPos, yIndexPos, sizeX, sizeY);
            return true;
        } else {
            return false;
        }

    }
}
