using UnityEngine;
using System;

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

    private static int[][] farmCosts = new int[fruitTypeCount][] {
        new int[5] {10,0,0,0,0},
        new int[5] {50,0,0,0,0},
        new int[5] {500,100,0,0,0},
        new int[5] {8500,600,90,0,0},
        new int[5] {44000,2600,340,120,0}
    };

    // farmCosts[0] = new int[5] {10,0,0,0,0};
    // farmCosts[1] = new int[5] {50,0,0,0,0};
    // farmCosts[2] = new int[5] {500,100,0,0,0};
    // farmCosts[3] = new int[5] {8500,600,90,0,0};
    // farmCosts[4] = new int[5] {44000,2600,340,120,0};

    private static int[][] levelUpCosts = new int[fruitTypeCount - 1][] {
        new int[5] {102,0,0,0,0},
        new int[5] {1000,200,0,0,0},
        new int[5] {21000,1350,180,0,0},
        new int[5] {87000,5600,700,250,0}
    };

    // levelUpCosts[0] = new int[5] {102,0,0,0,0};
    // levelUpCosts[1] = new int[5] {1000,200,0,0,0};
    // levelUpCosts[2] = new int[5] {21000,1350,180,0,0};
    // levelUpCosts[3] = new int[5] {87000,5600,700,250,0};

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

    private static int[] getTileCosts() {
        count = IslandManager.existingTileCount;
        int num1 = 
        return new int[fruitTypeCount] {
            50 + Math.Pow(0.5 * count, 1.8),
            30 + Math.Pow(0.5 * count, 1.8),
            20 + Math.Pow(0.5 * count, 1.8),
            50 + Math.Pow(0.5 * count, 1.8),
            1 + Math.Pow(0.5 * count, 1.8)
        };
    } 

    public static bool hasEnoughFruitsToBuildTile() {
        return compareArrays(fruitCounts, tileCosts);
    }

    public static void buildTile() {
        subtractArrays(fruitCounts, tileCosts);
        IslandManager.existingTileCount++;
    }

    public static bool hasEnoughFruitsToBuildFarm(Fruit fruit) {
        return compareArrays(fruitCounts, farmCosts[(int) fruit]);
    }

    public static void buildFarm(Fruit fruit) {
        subtractArrays(fruitCounts, farmCosts[(int) fruit]);
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
