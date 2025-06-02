using UnityEngine;
using System.Collections.Generic; // Required for using Lists

public class statsDto
{
    public float damage { get; set; }
    public float hp { get; set; }
    public bool qty { get; set; }
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Camera mainCamera;
    public LayerMask hitLayers;
    public string spawnSpotLayerName = "SpawnSpotLayer"; 
    private List<GameObject> spawnSpots;
	int damage=10;
	int hp=10;
	int enemyQtyPerWave=10;

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
		Debug.Log(spawnSpots[0].name);
		statsDto stats= new statsDto();
		stats.qty=3;
		spawnWave(stats);
    }
	
	void spawnWave(statsDto stats){
			
			
            if (spawnSpots.Count > 0)
            {
                Debug.Log($"Found {spawnSpots.Count} spawn spots on layer '{spawnSpotLayerName}':");
				for( int i=0;i<stats.qty;i++){
					GameObject spawnSpot = spawnSpots[i%spawnSpots.length];
					
					Instantiate(objectToSpawn,spawnSpot.position, Quaternion.identity);

				}
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