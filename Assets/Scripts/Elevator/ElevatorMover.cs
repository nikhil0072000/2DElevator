using System.Collections;
using UnityEngine;

public class ElevatorMover : MonoBehaviour
{
    // Handle elevator movement  to target position 
    public IEnumerator MoveTo(Vector3 targetPosition, float speed)
    {
       
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f) // check the distance between current position and target position
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
            yield return null;
        }
    }
}
