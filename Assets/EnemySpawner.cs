using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 1. Reference to the Prefab you want to spawn
    public GameObject objectToSpawn;

    // 2. Reference to your main camera (optional, can be auto-detected)
    public Camera mainCamera;

    // 3. Optional: LayerMask to specify which layers the ray should hit
    // This helps ignore things like UI elements or specific invisible colliders.
    public LayerMask hitLayers;

    void Start()
    {
        // If mainCamera is not assigned, try to find the main camera
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
    }

    void Update()
    {
        // Check for left mouse button click (0 is left, 1 is right, 2 is middle)
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera going through the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo; // Variable to store information about what the ray hits

            // Perform the raycast
            // We use a max distance (e.g., 100f) to avoid infinite rays
            // We also use the hitLayers if specified
            if (Physics.Raycast(ray, out hitInfo, 100f, hitLayers))
            {
                // If the ray hits something on the specified layers...
                Debug.Log("Ray hit: " + hitInfo.collider.gameObject.name + " at point: " + hitInfo.point);

                // Spawn the prefab at the hit point with a default rotation (Quaternion.identity)
                // You can customize the rotation if needed, e.g., Quaternion.LookRotation(hitInfo.normal)
                Instantiate(objectToSpawn, hitInfo.point, Quaternion.identity);
            }
            else
            {
                // Optional: Handle cases where the ray doesn't hit anything (e.g., clicking the sky)
                Debug.Log("Ray did not hit any object on the specified layers.");
            }
        }
    }
}