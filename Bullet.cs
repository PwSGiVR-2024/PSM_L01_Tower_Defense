using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    [SerializeField] private float speed = 20f;
    private float destroyDelay = 0f;
    private Vector3 targetPosition;
    private float damage;
    private float baseDmg = 10;
    private float modifier = 0;
    private Rigidbody rb; // Nowe: Dodano Rigidbody

    private void Start()
    {
        damage = baseDmg + modifier;
        rb = GetComponent<Rigidbody>(); // Pobranie Rigidbody
        if (rb == null)
        {
            Debug.LogError("Brak Rigidbody na pocisku! Dodaj Rigidbody w Inspectorze.");
        }

        Destroy(gameObject, lifetime); // Zniszczenie po czasie
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            transform.Translate((targetPosition - transform.position).normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Pocisk trafi³ w: {other.gameObject.name}"); // Debugowanie kolizji

        if (other.CompareTag("Enemy"))
        {
            DamageControler enemy = other.GetComponent<DamageControler>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject, destroyDelay);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
