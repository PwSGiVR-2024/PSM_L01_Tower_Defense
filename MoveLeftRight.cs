using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Pr�dko�� ruchu
    [SerializeField] private float distance = 5f; // Maksymalna odleg�o�� ruchu

    private Vector3 startPosition;
    private int direction = 1; // 1 = prawo, -1 = lewo

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.forward * direction * speed * Time.deltaTime;

        // Je�li obiekt przekroczy zakres ruchu, zmie� kierunek
        if (Mathf.Abs(transform.position.z - startPosition.z) >= distance)
        {
            direction *= -1;
        }
    }
}
