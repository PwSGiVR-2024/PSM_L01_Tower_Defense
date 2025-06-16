using UnityEngine;

public class Tower_fast_shoot : MonoBehaviour
{
    private Transform wrog;
    private Transform player;
    private GameObject prefab;
    private Transform bulletSpawnPoint;

    [SerializeField] private int odlegloscTrigger = 10; // Odleg�o��, w kt�rej bro� ma strzela�
    [SerializeField] private float fireRate = 0.2f; // Czas mi�dzy strza�ami
    private float lastShotTime = 0f; // Czas ostatniego strza�u

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("boom")?.transform;
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
        }
        else
        {
            wrog = null;
        }
    }
}
