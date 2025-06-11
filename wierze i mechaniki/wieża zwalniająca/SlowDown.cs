using UnityEngine;

public class SlowDown : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float slowPercentage = 0.25f;
    [SerializeField] private float lifetime = 5f;

    private Transform target;

    public void SetTarget(Transform targetTransform)
    {
        Debug.Log("[SlowDown] Cel ustawiony: " + targetTransform.name);
        target = targetTransform;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[SlowDown] Zderzenie z {other.name}");

        MoveLeftRight movement = other.GetComponent<MoveLeftRight>();
        if (movement != null)
        {
            Debug.Log("[SlowDown] Znalaz³em MoveLeftRight!");
            movement.ReduceSpeedByPercentage(slowPercentage);
        }

        Destroy(gameObject);
    }
}
