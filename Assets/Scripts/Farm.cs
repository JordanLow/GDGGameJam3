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

    private float[,] harvestDelays;
    private int[,] harvestAmounts;
    private bool isHarvestable;

    private ResourceManager.Fruit fruit;

    void Start() {
        startHarvestCycle();
    }

    public void startHarvestCycle() {
        Invoke(nameof(setHarvestable), harvestDelays[(int) fruit, ResourceManager.level]);
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
    }

    private void setHarvested() {
        this.isHarvestable = true;
        harvestedStateTexture.SetActive(true);
        harvestableStateTexture.SetActive(false);
    }

    public void harvest() {
        if (isHarvestable) {
            ResourceManager.addResource(fruit, harvestAmounts[(int) fruit, ResourceManager.level]);
            this.setHarvested();
            startHarvestCycle();
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