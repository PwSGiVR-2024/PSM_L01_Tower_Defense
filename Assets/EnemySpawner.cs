using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     public GameObject objectToSpawn;

    public Camera mainCamera;

    public LayerMask hitLayers;
    private List<GameObject> spawnSpots;
    

    void Start()
    {
         if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera == null)
        {
            Debug.LogError("ClickToSpawn: Main Camera not found! Please assign it in the inspector.");
            enabled = false; // Disable the script if no camera
        }

        if (objectToSpawn == null)
        {
            Debug.LogError("ClickToSpawn: Object To Spawn not assigned! Please assign it in the inspector.");
            enabled = false; // Disable script if no prefab
        }
        int spawnSpotLayer = LayerMask.NameToLayer("SpawnSpotLayer");
        if (spawnSpotLayer != -1)
        {
            spawnSpots = LayerUtils.GetObjectsInLayer(spawnSpotLayer);
            Debug.Log(spawnSpots[0].name);
            // Process enemies
        }
    }

    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo; // Variable to store information about what the ray hits

            if (Physics.Raycast(ray, out hitInfo, 100f, hitLayers))
            {
               Debug.Log("Ray hit: " + hitInfo.collider.gameObject.name + " at point: " + hitInfo.point);

                Instantiate(objectToSpawn, hitInfo.point, Quaternion.identity);
            }
            else
            {
               Debug.Log("Ray did not hit any object on the specified layers.");
            }
        }
    }
}