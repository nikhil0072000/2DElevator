using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public int floorNumber; 

    public enum Direction { Up, Down }
    public Direction direction; 

    public ElevatorManager manager; 

    // Handle Floorbutton press
    public void OnButtonPressed()
    {
        Debug.Log("Call from Floor " + floorNumber + " Direction: " + direction);
        manager.RequestElevator(floorNumber, direction);
    }
}
