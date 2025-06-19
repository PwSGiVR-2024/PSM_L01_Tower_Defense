using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float damage = 100f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private LayerMask enemyLayer;  // warstwa przeciwników

    private Transform target;

    public void SetTarget(Transform targetTransform)
    {
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
        // Sprawdzamy czy warstwa obiektu jest w enemyLayer
        if (((1 << other.gameObject.layer) & enemyLayer) == 0)
        {
            return; // ignoruj, to nie wróg
        }

        // Opcjonalnie sprawdzenie czy to nasz cel
        if (other.transform != target) return;

        Debug.Log($"Bomba trafi³a: {other.name}");

        ExplodeNow();
        Destroy(gameObject);
    }

    private void ExplodeNow()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        int hitCount = 0;

        foreach (Collider hit in colliders)
        {
            DamageControler dc = hit.GetComponent<DamageControler>();
            if (dc == null)
                dc = hit.GetComponentInParent<DamageControler>();

            if (dc != null)
            {
                dc.TakeDamage(damage);
                hitCount++;
            }
        }

        Debug.Log($"Eksplozja! Trafionych przeciwników: {hitCount}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
