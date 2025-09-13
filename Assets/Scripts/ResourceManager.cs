using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ResourceManager : MonoBehaviour
{

    private static InputSystem_Actions inputs;

    private static int numFruits = 2;

    private static int[] fruitCounts = new int[numFruits];

    private static int[][] tileCosts = new int[9800][];

    public static int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputs = new InputSystem_Actions();

        inputs.Enable();
        
        inputs.Player.Click.performed += _ => HandleClick();
    }

    private static void HandleClick()
    {

        // Get the screen position of the mouse
        Vector2 screenPos = Mouse.current.position.ReadValue();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Pointer is over a UI element (like your popup button)
            // Don't cast physics ray / don't interact with world object
            return;
        }

        Debug.Log("Postition: " + (screenPos.x).ToString() + ", " + (screenPos.y).ToString());

        Vector2 worldPos  = Camera.main.ScreenToWorldPoint(screenPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        
        if (hit.collider != null)
        {
            Debug.Log("Clicked 2D object: " + hit.collider.name);
            
            Tile tile = hit.collider.GetComponent<Tile>();
            
            tile.openClickMenu();
        }
    }

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

    public static bool hasEnoughFruitsToBuildTile(int tileCount) {
        return compareArrays(fruitCounts, tileCosts[tileCount]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
