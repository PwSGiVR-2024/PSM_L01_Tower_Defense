using UnityEngine;

public class SlowShot : MonoBehaviour
{
    public LayerMask enemyLayer;
    [SerializeField] private float triggerDistance = 10f;
    [SerializeField] private float fireRate = 2.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    private float lastShotTime = 0f;
    private Transform currentTarget;

    void Update()
    {
        FindNearestEnemy();

        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            if (distance <= triggerDistance && Time.time - lastShotTime >= fireRate)
            {
                ShootAt(currentTarget);
                lastShotTime = Time.time;
            }
        }
    }

    private void FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, triggerDistance, enemyLayer);
        float closestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                nearest = hit.transform;
            }
        }

        currentTarget = nearest;
    }

    private void ShootAt(Transform target)
    {
        if (bulletPrefab == null || shootPoint == null || target == null)
        {
            Debug.LogWarning("Brakuje prefab pocisku / punktu strza³u / celu.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        SlowDown bulletScript = bullet.GetComponent<SlowDown>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
            bulletScript.enemyLayer = enemyLayer; // Przekazujemy warstwê do AOE
        }
        else
        {
            Debug.LogWarning("Brak komponentu SlowDown na prefabie pocisku!");
        }
    }
}
