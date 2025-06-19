using UnityEngine;

public class build_controller : MonoBehaviour
{
    public void Build(GameObject towerPrefab, Transform buildPosition)
    {
        if (towerPrefab != null && buildPosition != null)
        {
            Instantiate(towerPrefab, buildPosition.position, Quaternion.identity);
            Debug.Log("Wie¿a zbudowana przez build_controller.");
        }
        else
        {
            Debug.LogError("Prefab wie¿y lub pozycja budowy s¹ puste!");
        }
    }
}
