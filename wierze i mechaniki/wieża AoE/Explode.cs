using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float lifetime = 10f;

    private Transform target;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime); // awaryjne zniszczenie pocisku
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
        // tylko jeœli uderzy³o dok³adnie w cel
        if (other.transform != target) return;

        ExplodeNow();
        Destroy(gameObject);
    }

    private void ExplodeNow()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        int hitCount = 0;

        foreach (Collider hit in colliders)
        {
            DamageControler dc = hit.GetComponent<DamageControler>();
            if (dc == null)
            {
                dc = hit.GetComponentInParent<DamageControler>();
            }

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
