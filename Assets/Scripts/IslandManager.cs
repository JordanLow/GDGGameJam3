using UnityEngine;
using UnityEngine.InputSystem;

public class IslandManager : MonoBehaviour
{

    private static int maxSizeX = 99;
    private static int maxSizeY = 99;
    private static int xOrigin = (maxSizeX - 1) / 2;
    private static int yOrigin = (maxSizeY - 1) / 2;

    private static Tile[,] tiles = new Tile[maxSizeX, maxSizeY];

    public static int existingTileCount = 1;

    [SerializeField] public GameObject tilePrefab;

    private Tile spawnTilePrefab(Vector3 position)
    {
        Tile instance = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<Tile>();
        Debug.Log("spawning at " + instance.transform.position);
        return instance;
    }

    void Start() {
        
        for (int i = 0; i < maxSizeX; i++) {
            for (int j = 0; j < maxSizeY; j++) {
                tiles[i, j] = spawnTilePrefab((i*(new Vector3(0.21f, 0.14f, 0)) + (j*(new Vector3(-0.21f, 0.14f, 0)))));
                tiles[i, j].setEmpty();
                tiles[i, j].setIslandManager(this);
                tiles[i, j].setIndexPosition(i, j);
            }
        }

        for (int i = xOrigin - 2; i < xOrigin + 3; i++) {
            for (int j = yOrigin - 2; j < yOrigin + 3; j++) {
                tiles[i, j].setFree();
            }
        }

        tiles[xOrigin, yOrigin].declareTownHall();
        tiles[xOrigin, yOrigin].name = "Town Hall";


        tiles[xOrigin + 3, yOrigin - 2].setAdd();
        tiles[xOrigin + 3, yOrigin - 1].setAdd();
        tiles[xOrigin + 3, yOrigin    ].setAdd();
        tiles[xOrigin + 3, yOrigin + 1].setAdd();
        tiles[xOrigin + 3, yOrigin + 2].setAdd();
        
        tiles[xOrigin - 3, yOrigin - 2].setAdd();
        tiles[xOrigin - 3, yOrigin - 1].setAdd();
        tiles[xOrigin - 3, yOrigin    ].setAdd();
        tiles[xOrigin - 3, yOrigin + 1].setAdd();
        tiles[xOrigin - 3, yOrigin + 2].setAdd();
        
        tiles[xOrigin - 2, yOrigin + 3].setAdd();
        tiles[xOrigin - 1, yOrigin + 3].setAdd();
        tiles[xOrigin    , yOrigin + 3].setAdd();
        tiles[xOrigin + 1, yOrigin + 3].setAdd();
        tiles[xOrigin + 2, yOrigin + 3].setAdd();
        
        tiles[xOrigin - 2, yOrigin - 3].setAdd();
        tiles[xOrigin - 1, yOrigin - 3].setAdd();
        tiles[xOrigin    , yOrigin - 3].setAdd();
        tiles[xOrigin + 1, yOrigin - 3].setAdd();
        tiles[xOrigin + 2, yOrigin - 3].setAdd();
    } 

    public bool checkTilesFree(int x, int y, int dx, int dy) {
        for (int i = x; i < x + dx; i++) {
            for (int j = y; j < y + dy; j++) {
                if (!tiles[i, j].isFree()) {
                    return false;
                }
            }
        }
        return true;
    }

    public void setTilesUsed(Farm farm, int x, int y, int dx, int dy) {
        for (int i = x; i < x + dx; i++) {
            for (int j = y; j < y + dy; j++) {
                tiles[i, j].setFarm(farm);
                tiles[i, j].setUsed();
            }
        }
    }

    public void setTilesFree(int x, int y, int dx, int dy) {
        for (int i = x; i < x + dx; i++) {
            for (int j = y; j < y + dy; j++) {
                tiles[i, j].setFarm(null);
                tiles[i, j].setFree();
            }
        }
    }

}