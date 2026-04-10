using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public int floorNumber; 

    public enum Direction { Up, Down }
    public Direction direction; 

    public ElevatorManager manager; 

    // Handle Floor button press Up and Down
    public void OnButtonPressed()
    {
        Debug.Log("Call from Floor " + floorNumber + " Direction: " + direction);
        manager.RequestElevator(floorNumber, direction);
    }
}
