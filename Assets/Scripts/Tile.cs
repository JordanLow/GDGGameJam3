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

    [SerializeField] public GameObject addStateTexture;
    [SerializeField] public GameObject existStateTexture;
    [SerializeField] public GameObject townHallStateTexture;
    [SerializeField] public GameObject tcollider;

    [SerializeField] public List<GameObject> farmPrefabs;

    [SerializeField] public GameObject menuPrefab;

    private int[,] farmSizes = new int[ResourceManager.fruitTypeCount, 2] {{1, 1}, {1, 2}, {2, 3}, {3, 2}, {3, 4}};

    private IslandManager islandManager;

    // private TileState state = TileState.Empty;
    // TEST
    // private TileState state = TileState.Used;
    private TileState state = TileState.Free;
    // private TileState state = TileState.Add;
    // ENDTEST

    private bool isTownHall = false;
    
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

    public void declareTownHall() {
        this.isTownHall = true;
        this.setUsed();
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(false);
        townHallStateTexture.SetActive(true);
    }

    public bool getIsTownHall() {
        return this.isTownHall;
    }

    public void setIndexPosition(int x, int y) {
        this.xIndexPos = x;
        this.yIndexPos = y;
    }

    public void setEmpty() {
        this.state = TileState.Empty;
        tcollider.SetActive(false);
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(false);
        townHallStateTexture.SetActive(false);
    }

    public void setAdd() {
        this.state = TileState.Add;
        tcollider.SetActive(true);
        addStateTexture.SetActive(true);
        existStateTexture.SetActive(false);
        townHallStateTexture.SetActive(false);

    }

    public void setFree() {
        this.state = TileState.Free;
        tcollider.SetActive(true);
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(true);
        townHallStateTexture.SetActive(false);
    }

    public void setUsed() {
        this.state = TileState.Used;
        tcollider.SetActive(true);
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(false);
        townHallStateTexture.SetActive(false);
    }

    public bool isFree() {
        return this.state == TileState.Free;
    }

    public bool buildTile() {
        if (ResourceManager.hasEnoughFruitsToBuildTile()) {
            ResourceManager.buildTile();
            this.setFree();
        }
        Debug.Log("Not enough resources");
        return false;
    }

    public void levelUp() {
        if (ResourceManager.level < 5) {
            if (ResourceManager.hasEnoughFruitsToLevelUp()) {
                ResourceManager.levelUp();
            } else {
                Debug.Log("Not enough resources");
            }

        } else {
            Debug.Log("Cannot level up anymore");
        }
    }

    public void harvestFarmOnTile() {
        currentFarm.harvest();
    }

    public void demolishFarmOnTile() {
        currentFarm.demolish();
    }

    public void openClickMenu() {
        PopupManager.Instance.ShowMenuForObject(this, menuPrefab);
        if (this.state == TileState.Used) {
            if (isTownHall) {
                PopupManager.Instance.EnableLevelUp();
            } else {
                PopupManager.Instance.EnableHarvestButton();
                PopupManager.Instance.EnableDemolishButton();
            }
        }
        if (this.state == TileState.Free) {
            int idx = 5; // ResourceManager.level?
            PopupManager.Instance.EnableBuildButton(idx);
        } 
        if (this.state == TileState.Add) {
            PopupManager.Instance.EnableAddButton();
        }
    }

    public bool buildFarm(int idx) {
        int sizeX = farmSizes[idx, 0];
        int sizeY = farmSizes[idx, 1];
        ResourceManager.Fruit fruit = (ResourceManager.Fruit) idx;
        if (islandManager.checkTilesFree(xIndexPos, yIndexPos, sizeX, sizeY)) {
            if (ResourceManager.hasEnoughFruitsToBuildFarm(fruit)) {
                currentFarm = spawnFarmPrefab(farmPrefabs[idx], new Vector3(xPos, yPos, 0));
                currentFarm.setFruit(fruit);
                currentFarm.setIslandManager(islandManager);
                currentFarm.setUsedTiles(xIndexPos, yIndexPos, sizeX, sizeY);
                ResourceManager.buildFarm(fruit);
                return true;
            } else {
                Debug.Log("Not enough fruits");
                return false;
            }
        } else {
            Debug.Log("Not enough space");
            return false;
        }

    }
}
