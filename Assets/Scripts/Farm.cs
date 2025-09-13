using UnityEngine;


public class Farm : MonoBehaviour
{
    private int xIndexPos;
    private int yIndexPos;
    private int sizeX;
    private int sizeY;
    
    private IslandManager islandManager;

    [SerializeField] public GameObject harvestedStateTexture;
    [SerializeField] public GameObject harvestableStateTexture;

    private float[,] harvestDelays = {{4,0,0,0,0},{8,13,0,0,0},{21,32,60,0,0},{39,63,107,148,0},{57,99,159,198,269}};
    private float[,] devharvestDelays = {{0.1f,0,0,0,0},{0.1f,0.1f,0,0,0},{0.1f,0.1f,0.1f,0,0},{0.1f,0.1f,0.1f,0.1f,0},{0.1f,0.1f,0.1f,0.1f,0.1f}};
    private int[,] harvestAmounts = {{7,0,0,0,0},{21,9,0,0,0},{63,27,10,0,0},{189,81,30,11,0},{567,243,90,33,15}};
    private bool isHarvestable = false;

    private ResourceManager.Fruit fruit;

    void Start() {
        startHarvestCycle();
    }

    public void startHarvestCycle() {
        Invoke(nameof(setHarvestable), devharvestDelays[(int) fruit, ResourceManager.level - 1]);
    }

    public void setFruit(ResourceManager.Fruit fruit) {
        this.fruit = fruit;
    }

    public bool getIsHarvestable() {
        return isHarvestable;
    }

    private void setHarvestable() {
        this.isHarvestable = true;
        harvestedStateTexture.SetActive(false);
        harvestableStateTexture.SetActive(true);
        Debug.Log("Can Harvest");
    }

    private void setHarvested() {
        this.isHarvestable = true;
        harvestedStateTexture.SetActive(true);
        harvestableStateTexture.SetActive(false);
    }

    public void harvest() {
        if (isHarvestable) {
            ResourceManager.addResource(fruit, harvestAmounts[(int) fruit, ResourceManager.level - 1]);
            this.setHarvested();
            startHarvestCycle();
            Debug.Log("Harvested");
        } else {
            Debug.Log("Not ready to harvest");
        }
    }

    public void setUsedTiles(int x, int y, int dx, int dy) {
        this.xIndexPos = x;
        this.yIndexPos = y;
        this.sizeX = dx;
        this.sizeY = dy;
        islandManager.setTilesUsed(this, xIndexPos, yIndexPos, sizeX, sizeY);
    }

    public void setIslandManager(IslandManager islandManager) {
        this.islandManager = islandManager;
    }

    public void demolish() {
        islandManager.setTilesFree(xIndexPos, yIndexPos, sizeX, sizeY);
        Destroy(this.gameObject);
    }

}