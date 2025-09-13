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

    private static Tile SpawnTilePrefab(Vector3 position)
    {
        Tile instance = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<Tile>();
        return instance;
    }

    void Start() {
        
        for (int i = 0; i < maxSizeX; i++) {
            for (int j = 0; j < maxSizeY; j++) {
                tiles[i, j] = SpawnTilePrefab(new Vector3(0, 0, 0));
                tiles[i, j].setEmpty();
            }
        }
        tiles[xOrigin, yOrigin].setExist();

        tiles[xOrigin + 1, yOrigin].setAdd();
        tiles[xOrigin, yOrigin + 1].setAdd();
        tiles[xOrigin - 1, yOrigin].setAdd();
        tiles[xOrigin, yOrigin - 1].setAdd();
    } 


}