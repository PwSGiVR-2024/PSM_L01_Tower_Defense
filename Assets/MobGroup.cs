using UnityEngine;
using UnityEngine.Rendering;

public class MobGroup
{
    public GameObject MobPrefab; 
    public int MobQty;
    public int spawnSpotId;
}

public class TroyanMobGroup : MobGroup
{
    public TroyanMobGroup()
    {
        MobPrefab = Resources.Load<GameObject>("Prefab/TroyanEnemy");

        if (MobPrefab == null)
        {
            Debug.LogError("Failed to load TroyanEnemy Variant prefab. Check the path and ensure it's in a Resources folder.");
        }
    }
}