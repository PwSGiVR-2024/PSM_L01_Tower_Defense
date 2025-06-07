using UnityEngine;
using System.Collections; // Required for using Coroutines (IEnumerator)
using System.Collections.Generic;

public class statsDto
{
    public float damage { get; set; }
    public float hp { get; set; }
    public int qty { get; set; } // Corrected from bool
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Camera mainCamera;
    public LayerMask hitLayers;
    public string spawnSpotLayerName = "SpawnSpotLayer";
    
    // NEW: Add a public field for the delay so you can change it in the Inspector.
    public float spawnDelay = 0.5f; 

    private List<GameObject> spawnSpots;
    int damage = 10;
    int hp = 10;
    int enemyQtyPerWave = 10;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera == null)
        {
            Debug.LogError("EnemySpawner: Main Camera not found! Please assign it in the inspector or ensure a Camera is tagged 'MainCamera'.");
            enabled = false;
            return;
        }

        if (objectToSpawn == null)
        {
            Debug.LogError("EnemySpawner: Object To Spawn not assigned! Please assign it in the inspector.");
            enabled = false;
            return;
        }
        spawnSpots = new List<GameObject>();
        int spawnSpotLayer = LayerMask.NameToLayer(spawnSpotLayerName);

        if (spawnSpotLayer == -1)
        {
            Debug.LogWarning($"EnemySpawner: Layer '{spawnSpotLayerName}' does not exist. Please ensure the layer is created in the Unity Editor (Edit > Project Settings > Tags and Layers).");
        }
        else
        {
            GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject go in allGameObjects)
            {
                if (go.layer == spawnSpotLayer)
                {
                    spawnSpots.Add(go);
                }
            }
        }

        Debug.Log(spawnSpots.Count > 0 ? spawnSpots[0].name : "No spawn spots found.");
        statsDto stats = new statsDto();
        stats.qty = 15;

        // CHANGED: We now call StartCoroutine to run the spawn logic over time.
        StartCoroutine(SpawnWave(stats));
    }

    // CHANGED: The function is now an IEnumerator to allow for pausing.
    IEnumerator SpawnWave(statsDto stats)
    {
        if (spawnSpots.Count > 0)
        {
            Debug.Log($"Found {spawnSpots.Count} spawn spots on layer '{spawnSpotLayerName}'. Starting wave...");
            for (int i = 0; i < stats.qty; i++)
            {
                // Corrected property from .length to .Count
                GameObject spawnSpot = spawnSpots[i % spawnSpots.Count];

                // Corrected property from .position to .transform.position
                Instantiate(objectToSpawn, spawnSpot.transform.position, Quaternion.identity);

                // NEW: This is the magic line. It pauses the coroutine here for the specified duration.
                // The rest of the game continues to run normally during this pause.
                yield return new WaitForSeconds(spawnDelay);
            }
            Debug.Log("Wave finished spawning.");
        }
        else
        {
            Debug.LogWarning($"EnemySpawner: No GameObjects found on layer '{spawnSpotLayerName}'.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100f, hitLayers))
            {
                Debug.Log("Ray hit: " + hitInfo.collider.gameObject.name + " at point: " + hitInfo.point + " on layer: " + LayerMask.LayerToName(hitInfo.collider.gameObject.layer));
                Instantiate(objectToSpawn, hitInfo.point, Quaternion.identity);
            }
            else
            {
                Debug.Log("Ray did not hit any object on the specified hitLayers.");
            }
        }
    }
}