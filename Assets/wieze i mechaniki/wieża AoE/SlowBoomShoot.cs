using UnityEngine;

public class SlowBoomShoot : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float triggerDistance = 10f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private GameObject bombaPrefab; // prefab pocisku nazywa siê "bomba"
    [SerializeField] private Transform shootPoint;  // punkt strza³u "shootPoint"

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
        if (bombaPrefab == null || shootPoint == null || target == null)
        {
            Debug.LogWarning("Brakuje prefab bomby / punktu shootPoint / celu.");
            return;
        }

        GameObject bomba = Instantiate(bombaPrefab, shootPoint.position, Quaternion.identity);
        Explode bombaScript = bomba.GetComponent<Explode>();
        if (bombaScript != null)
        {
            bombaScript.SetTarget(target);
            Debug.Log($"Bomba utworzona i cel ustawiony na: {target.name}");
        }
        else
        {
            Debug.LogWarning("Skrypt Explode nie znaleziony na prefabie bomby!");
        }
    }
}

