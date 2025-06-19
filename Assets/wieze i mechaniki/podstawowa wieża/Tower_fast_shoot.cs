using UnityEngine;

public class Tower_Fast_Shoot : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float triggerDistance = 10f;
    [SerializeField] private float fireRate = 2.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

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
        if (bulletPrefab == null || bulletSpawnPoint == null || target == null)
        {
            Debug.LogWarning("Brakuje prefab bullet / punktu spawn / celu.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>(); // <<< upewnij siê ¿e to Bullet!
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
            Debug.Log($"Pocisk utworzony i cel ustawiony na: {target.name}");
        }
        else
        {
            Debug.LogWarning("Skrypt Bullet nie znaleziony na prefabie!");
        }
    }
}
