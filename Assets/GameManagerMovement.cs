using System.Collections.Generic;
using UnityEngine;

public class GameManagerMovement : MonoBehaviour
{
    [Header("Path Points (Set via GUI)")]
    public List<Vector3> keyPoints;

    [Header("Objects to Move")]
    public List<MovableObject> movableObjects;

    void Start()
    {

        foreach (var movable in movableObjects)
        {
            movable.InitializeQueue(keyPoints);
            Debug.Log($"Added MovableObject: {movable.name}");
        }
    }

    void Update()
    {
        foreach (var movable in movableObjects)
        {
            movable.Move();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (keyPoints != null && keyPoints.Count > 1)
        {
            for (int i = 0; i < keyPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(keyPoints[i], keyPoints[i + 1]);
            }
        }
    }
}