using UnityEngine;
using TMPro;

public class ElevatorDisplay : MonoBehaviour
{
    public ElevatorController elevator; 
    public TextMeshProUGUI text; 
    private void Update()
    {
        // Display current floor in the Text 
        if (elevator != null && text != null)
        {
            text.text = "Floor: " + elevator.currentFloor;
        }
    }
}
