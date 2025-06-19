using UnityEngine;

public class SlowBoomShoot : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private float triggerDistance = 10f;
    [SerializeField] private float fireRate = 5f;
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                nearest = enemy.transform;
            }
        }

        currentTarget = nearest;
    }

    private void ShootAt(Transform target)
    {
        if (bulletPrefab == null || bulletSpawnPoint == null || target == null) return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Explode bulletScript = bullet.GetComponent<Explode>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
        else
        {
            Debug.LogWarning("Prefab nie ma komponentu Explode!");
        }
    }
}
