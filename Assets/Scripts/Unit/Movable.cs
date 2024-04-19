using System.Collections;
using UnityEngine;

public class Movable : MonoBehaviour, IMovable
{
    public float speed = 5f;  // Speed at which the unit moves

    public void Move(Vector2 newPosition)
    {
        StartCoroutine(MoveRoutine(newPosition));
    }

    private IEnumerator MoveRoutine(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
