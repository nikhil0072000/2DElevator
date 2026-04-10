using System.Collections;
using UnityEngine;

public class ElevatorMover : MonoBehaviour
{
    // Move elevator to target
    public IEnumerator MoveTo(Vector3 targetPosition, float speed)
    {
       
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
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
