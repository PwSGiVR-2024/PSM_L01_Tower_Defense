using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovableObject: MonoBehaviour
{
   
    public float speed = 5f;
    public float reachThreshold = 0.1f;

    private Queue<Vector3> pointsQueue;
    private Vector3 currentTarget;
    private bool hasTarget;

    public void InitializeQueue(List<Vector3> sharedPoints)
    {
        pointsQueue = new Queue<Vector3>(sharedPoints);
        SetNextTarget();
    }

    public void Move()
    {
        if (!hasTarget)
            return;
        Debug.Log("Moving object...");
        gameObject.transform.position = Vector3.MoveTowards(
            gameObject.transform.position,
            currentTarget,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(gameObject.transform.position, currentTarget) <= reachThreshold)
        {
            SetNextTarget();
        }
    }

    private void SetNextTarget()
    {
        if (pointsQueue.Count > 0)
        {
            currentTarget = pointsQueue.Dequeue();
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }
    }
}