using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 100f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private LayerMask enemyLayer;

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
        // Sprawdzenie warstwy
        if (((1 << other.gameObject.layer) & enemyLayer) == 0)
        {
            Debug.Log($"[Bullet] Zignorowano kolizjê z: {other.name}, warstwa: {LayerMask.LayerToName(other.gameObject.layer)}");
            return; // nie trafiono warstwy przeciwnika
        }

        Debug.Log($"[Bullet] Trafiono przeciwnika: {other.name}");

        DamageControler dc = other.GetComponent<DamageControler>();
        if (dc == null)
        {
            dc = other.GetComponentInParent<DamageControler>();
        }

        if (dc != null)
        {
            dc.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
