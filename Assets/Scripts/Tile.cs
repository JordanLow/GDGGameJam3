using UnityEngine;

public class Tile : MonoBehaviour
{

    enum TileState {
        Empty = 0,
        Add,
        Exist
    }

    enum Fruit {
        strawberry = 0,
        pineapple,
        watermelon
    }

    [SerializeField] public GameObject addStateTexture;
    [SerializeField] public GameObject existStateTexture;

    [SerializeField] public GameObject strawberryFarmPrefab;
    [SerializeField] public GameObject pineappleFarmPrefab;
    [SerializeField] public GameObject watermelonFarmPrefab;
    [SerializeField] public GameObject lemonFarmPrefab;
    [SerializeField] public GameObject appleFarmPrefab;
    [SerializeField] public GameObject pearFarmPrefab;
    [SerializeField] public GameObject grapeFarmPrefab;
    [SerializeField] public GameObject bananaFarmPrefab;
    [SerializeField] public GameObject kiwiFarmPrefab;
    [SerializeField] public GameObject mangoFarmPrefab;

    [SerializeField] public GameObject menuPrefab;



    private TileState state = TileState.Empty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void setExist() {
        this.state = TileState.Exist;
        addStateTexture.SetActive(false);
        existStateTexture.SetActive(true);
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
        return false;
    }
}
