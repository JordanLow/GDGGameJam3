using UnityEngine;
using TMPro;

public class UITracker : MonoBehaviour
{
    [SerializeField] private TMP_Text grapes;
    [SerializeField] private TMP_Text strawberry;
    [SerializeField] private TMP_Text pineapple;
    [SerializeField] private TMP_Text watermelon;
    [SerializeField] private TMP_Text mango;
    [SerializeField] private TMP_Text level;

    void Update()
    {
        grapes.text = ResourceManager.fruitCounts[0].ToString();
        pineapple.text = ResourceManager.fruitCounts[1].ToString();
        watermelon.text = ResourceManager.fruitCounts[2].ToString();
        strawberry.text = ResourceManager.fruitCounts[3].ToString();
        mango.text = ResourceManager.fruitCounts[4].ToString();
        level.text = ResourceManager.level.ToString();
    }

}