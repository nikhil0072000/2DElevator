using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public ElevatorController[] elevators;

    public void RequestElevator(int floor, FloorButton.Direction direction)
    {
        ElevatorController bestElevator = null;
        int minDistance = int.MaxValue;

        // Find best elevator
        foreach (var elevator in elevators)
        {
            if (!elevator.DecideDirection(floor, direction))
            {
                continue;
            }

            int distance = elevator.DistanceToFloor(floor);
            bool isIdle = elevator.IsIdle();

            if (bestElevator == null)
            {
                bestElevator = elevator;
                minDistance = distance;
                continue;
            }

            // Compare elevators
            bool bestIsIdle = bestElevator.IsIdle();
            if ((isIdle && !bestIsIdle) ||
                (isIdle == bestIsIdle && distance < minDistance))
            {
                bestElevator = elevator;
                minDistance = distance;
            }
        }

        if (bestElevator == null && elevators.Length > 0)
        {
            // Fallback selection
            foreach (var elevator in elevators)
            {
                int distance = elevator.DistanceToFloor(floor);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestElevator = elevator;
                }
            }
        }

        if (bestElevator != null)
        {
            bestElevator.AddRequest(floor, direction);
        }
        else
        {
            Debug.LogWarning("No elevators configured on ElevatorManager");
        }
    }
}
