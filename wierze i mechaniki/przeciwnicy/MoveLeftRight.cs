using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float distance = 5f;

    private Vector3 startPosition;
    private int direction = 1;
    private float currentSpeed;

    private void Start()
    {
        startPosition = transform.position;
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        transform.position += Vector3.forward * direction * currentSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.z - startPosition.z) >= distance)
        {
            direction *= -1;
        }
    }



    public void ReduceSpeedByPercentage(float percentage)
    {
        float slowAmount = baseSpeed * percentage;
        currentSpeed = Mathf.Max(baseSpeed - slowAmount, 0.5f);
        Debug.Log($"[MoveLeftRight] Prêdkoœæ przeciwnika zmniejszona do {currentSpeed}");
    }
}
