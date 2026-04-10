using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public int currentFloor = 0;
    public float speed = 2f;
    public float floorStopDelay = 1f;

    public List<int> requestQueue = new List<int>();

    private bool isMoving;
    private int targetFloor;
    private int direction; 

    public Transform[] floorPoints;
    public ElevatorMover mover;

    private void Awake()
    {
        if (mover == null)
        {
            mover = GetComponent<ElevatorMover>();
        }
    }

    private void Update()
    {
        if (isMoving || requestQueue.Count == 0)// if elevator is moving or there are no requests in the queue
        {
            return;
        }

        targetFloor = DequeueNextTarget();
        StartCoroutine(MoveToFloor(targetFloor));
    }


    public void AddRequest(int floor, FloorButton.Direction callDirection)
    {
        if (floor < 0 || floor >= floorPoints.Length)
        {
            Debug.LogWarning("Invalid floor request: " + floor);
            return;
        }

        // Ignore opposite calls
        if (IsMovingUp() && (callDirection == FloorButton.Direction.Down || floor < currentFloor))
        {
            return;
        }

        if (IsMovingDown() && (callDirection == FloorButton.Direction.Up || floor > currentFloor))
        {
            return;
        }

        if (!requestQueue.Contains(floor))
        {
            if (direction == 0)
            {
                direction = callDirection == FloorButton.Direction.Up ? 1 : -1;
            }

            requestQueue.Add(floor);
            SortQueueByDirection();
        }
    }

    private int DequeueNextTarget()
   {
    int next = requestQueue[0];
    requestQueue.RemoveAt(0);

    if (next > currentFloor)
    {
        direction = 1;
    }
    else if (next < currentFloor)
    {
        direction = -1;
    }

    return next;
    }

    private IEnumerator MoveToFloor(int floor)
    {
        isMoving = true;

        Vector3 targetPos = floorPoints[floor].position;
        yield return mover.MoveTo(targetPos, speed);

        currentFloor = floor;
        if (requestQueue.Count == 0)
        {
            direction = 0; 
        }
        else
        {
            direction = direction; 
        }

        yield return new WaitForSeconds(floorStopDelay);

        isMoving = false;
    }

    public bool IsIdle()
    {
        return !isMoving && requestQueue.Count == 0;
    }

    public int DistanceToFloor(int floor)
    {
        return Mathf.Abs(currentFloor - floor);
    }

    public bool IsMovingUp()
    {
        return direction > 0;
    }

    public bool IsMovingDown()
    {
        return direction < 0;
    }

    private void SortQueueByDirection()
    {
        if (direction == 0 && requestQueue.Count > 0)
        {
            direction = requestQueue[0] >= currentFloor ? 1 : -1;
        }

        if (direction >= 0)
        {
            requestQueue.Sort((a, b) => a.CompareTo(b));
        }
        else
        {
            requestQueue.Sort((a, b) => b.CompareTo(a));
        }
    }

    

    public bool DecideDirection(int floor, FloorButton.Direction callDirection)
    {
        if (IsIdle()) return true;

        if (IsMovingUp())
        {
            return callDirection == FloorButton.Direction.Up && floor >= currentFloor;
        }

        if (IsMovingDown())
        {
            return callDirection == FloorButton.Direction.Down && floor <= currentFloor;
        }

        return true;
    }
}
