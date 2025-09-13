using UnityEngine;
using UnityEngine.InputSystem;

public class IslandManager : MonoBehaviour
{

    private static int maxSizeX = 99;
    private static int maxSizeY = 99;
    private static int xOrigin = (maxSizeX - 1) / 2;
    private static int yOrigin = (maxSizeY - 1) / 2;

    private static Tile[,] tiles = new Tile[maxSizeX, maxSizeY];

    [SerializeField] public static GameObject tilePrefab;

    private static Tile spawnTilePrefab(Vector3 position)
    {
        Tile instance = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<Tile>();
        return instance;
    }

    void Start() {
        
        for (int i = 0; i < maxSizeX; i++) {
            for (int j = 0; j < maxSizeY; j++) {
                tiles[i, j] = spawnTilePrefab(new Vector3(0, 0, 0));
                tiles[i, j].setEmpty();
                tiles[i, j].setIslandManager(this);
                tiles[i, j].setIndexPosition(i, j);
            }
        }
        tiles[xOrigin, yOrigin].setFree();

        tiles[xOrigin + 1, yOrigin].setAdd();
        tiles[xOrigin, yOrigin + 1].setAdd();
        tiles[xOrigin - 1, yOrigin].setAdd();
        tiles[xOrigin, yOrigin - 1].setAdd();
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