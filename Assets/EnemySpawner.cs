using UnityEngine;
using System.Collections.Generic; // Required for using Lists

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Camera mainCamera;
    public LayerMask hitLayers;
    public string spawnSpotLayerName = "SpawnSpotLayer"; // Editable layer name in Inspector
    private List<GameObject> spawnSpots;

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
            // Find all GameObjects in the scene
            GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

            // Filter GameObjects by layer
            foreach (GameObject go in allGameObjects)
            {
                if (go.layer == spawnSpotLayer)
                {
                    spawnSpots.Add(go);
                }
            }

            // Optional: Log the found spawn spots
            if (spawnSpots.Count > 0)
            {
                Debug.Log($"Found {spawnSpots.Count} spawn spots on layer '{spawnSpotLayerName}':");
                foreach (GameObject spot in spawnSpots)
                {
                    Debug.Log("- " + spot.name);
                }
            }
            else
            {
                Debug.LogWarning($"EnemySpawner: No GameObjects found on layer '{spawnSpotLayerName}'.");
            }
        }
		Debug.Log(spawnSpots[0].name);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo; // Variable to store information about what the ray hits

            // Ensure hitLayers is not 0 (Nothing) or -1 (Everything) if you intend to filter by specific layers for spawning
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