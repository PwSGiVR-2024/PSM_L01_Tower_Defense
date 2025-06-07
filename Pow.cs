using UnityEngine;

public class Pow : MonoBehaviour
{
    private Transform wrog;
    private Transform player;
    private GameObject prefab;
    private Transform bulletSpawnPoint;

    [SerializeField] private int odlegloscTrigger = 10; // Odleg³oœæ, w której broñ ma strzelaæ
    [SerializeField] private float fireRate = 1f; // Czas miêdzy strza³ami
    private float lastShotTime = 0f; // Czas ostatniego strza³u

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        prefab = Resources.Load<GameObject>("BulletLite_01");
        bulletSpawnPoint = GameObject.FindGameObjectWithTag("boom")?.transform;

        if (prefab == null)
        {
            Debug.LogError("Nie znaleziono prefabrykatu pocisku!");
        }
        if (bulletSpawnPoint == null)
        {
            Debug.LogError("Nie znaleziono punktu startowego pocisku!");
        }

        FindEnemy();
    }

    void Update()
    {
        if (wrog != null)
        {
            float distanceToEnemy = Vector3.Distance(wrog.position, player.position);

            if (distanceToEnemy <= odlegloscTrigger && Time.time - lastShotTime >= fireRate)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
        else
        {
            FindEnemy();
        }
    }

    private void Shoot()
    {
        if (wrog == null || prefab == null || bulletSpawnPoint == null) return;

        Vector3 spawnPosition = bulletSpawnPoint.position;
        Quaternion spawnRotation = bulletSpawnPoint.rotation;

        GameObject bulletInstance = Instantiate(prefab, spawnPosition, spawnRotation);

        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(wrog.position);
        }
    }

    private void FindEnemy()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject != null)
        {
            wrog = enemyObject.transform;
            Debug.Log("Znaleziono przeciwnika: " + enemyObject.name);
        }
        else
        {
            wrog = null;
            Debug.Log("Nie znaleziono przeciwnika. Oczekiwanie...");
        }
    }
}
