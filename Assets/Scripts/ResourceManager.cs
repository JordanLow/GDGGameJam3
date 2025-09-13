using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public const int fruitTypeCount = 5;

    public enum Fruit {
        Strawberry = 0,
        Pineapple,
        Grape,
        Watermelon,
        Mango
    }

    private static int[] fruitCounts = new int[fruitTypeCount];

    private static int[][] tileCosts = new int[9800][];

    private static int[][] levelUpCosts = new int[9][];

    public static int level = 1;

    private static bool compareArrays(int[] arr1, int[] arr2) {
        if (arr1.Length != arr2.Length) {
            Debug.Log("Uncomparable Arrays");
            return false;
        } else {
            for (int i = 0; i < arr1.Length; i++) {
                if (arr1[i] < arr2[i]) {
                    return false;
                }
            }
            return true;
        }
    }

    private static void subtractArrays(int[] arr1, int[] arr2) {
        if (arr1.Length != arr2.Length) {
            Debug.Log("Uncomparable Arrays");
        } else {
            for (int i = 0; i < arr1.Length; i++) {
                arr1[i] -= arr2[i];
            }
        }
    }

    public static bool hasEnoughFruitsToBuildTile() {
        return compareArrays(fruitCounts, tileCosts[IslandManager.existingTileCount]);
    }

    public static void buildTile() {
        subtractArrays(fruitCounts, tileCosts[IslandManager.existingTileCount]);
        IslandManager.existingTileCount++;
    }

    public static bool hasEnoughFruitsToLevelUp() {
        return compareArrays(fruitCounts, levelUpCosts[level - 1]);
    }

    public static void levelUp() {
        subtractArrays(fruitCounts, levelUpCosts[level - 1]);
        level++;
    }

    public static void addResource(Fruit fruit, int count) {
        fruitCounts[(int) fruit] += count;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
