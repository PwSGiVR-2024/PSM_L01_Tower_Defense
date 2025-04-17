using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Prêdkoœæ ruchu
    [SerializeField] private float distance = 5f; // Maksymalna odleg³oœæ ruchu

    private Vector3 startPosition;
    private int direction = 1; // 1 = prawo, -1 = lewo

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.forward * direction * speed * Time.deltaTime;

        // Jeœli obiekt przekroczy zakres ruchu, zmieñ kierunek
        if (Mathf.Abs(transform.position.z - startPosition.z) >= distance)
        {
            direction *= -1;
        }
    }
}
