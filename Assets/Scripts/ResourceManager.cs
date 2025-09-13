using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceManager : MonoBehaviour
{

    private static InputSystem_Actions inputs;

    private static int numFruits = 2;

    // enum Fruit
    // {
    //     Apple = 0,
    //     Grape
    // }

    private static int[] fruitCount = new int[numFruits];

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
