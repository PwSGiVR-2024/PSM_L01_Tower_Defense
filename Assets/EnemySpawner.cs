using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Level Settings")]
    public LevelDefinition currentLevel;

    [Header("Spawn Points")]
    public string spawnSpotLayerName = "SpawnSpotLayer";
    
    private List<Transform> spawnSpots;
    private List<GameObject> activeEnemies = new List<GameObject>(); 
    private int currentWaveIndex = 0;

    void Start()
    {
        InitializeSpawnSpots();
        
        if (currentLevel == null)
        {
            Debug.LogError("Level Definition not assigned in the spawner!");
            return;
        }

        if (spawnSpots.Count == 0)
        {
            Debug.LogError("No spawn spots found on layer: " + spawnSpotLayerName);
            return;
        }

        StartCoroutine(RunLevel());
    }
    
    private void InitializeSpawnSpots()
    {
        spawnSpots = new List<Transform>();
        int spawnSpotLayer = LayerMask.NameToLayer(spawnSpotLayerName);

        if (spawnSpotLayer == -1)
        {
            Debug.LogWarning($"Layer '{spawnSpotLayerName}' does not exist. Please create it in Project Settings > Tags and Layers.");
            return;
        }

        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allGameObjects)
        {
            if (go.layer == spawnSpotLayer)
            {
                spawnSpots.Add(go.transform);
            }
        }
    }

    private IEnumerator RunLevel()
    {
        Debug.Log($"Level Starting: {currentLevel.levelName}");

        foreach (var wave in currentLevel.waves)
        {
            if (wave.delayBeforeWave > 0)
            {
				Debug.Log($"waves in the level: {currentLevel.waves.Count}");
                yield return new WaitForSeconds(wave.delayBeforeWave);
            }
            
            Debug.Log($"Wave Starting: {wave.waveName}");
            
            yield return StartCoroutine(SpawnWave(wave));
      
            yield return new WaitForSeconds(wave.delayBeforeNextWave);
            
            Debug.Log($"Wave {wave.waveName} cleared!");
            currentWaveIndex++;
        }
        
        Debug.Log($"LEVEL {currentLevel.levelName} COMPLETE!");
    }
    private IEnumerator SpawnWave(Wave wave)
    {
            Debug.Log($"spawning wave: {wave.waveName} ");
        foreach (var group in wave.enemyGroups)
        {
			yield return StartCoroutine(SpawnEnemyGroup(group));
			
        }
    }
    
    private IEnumerator SpawnEnemyGroup(EnemyGroup group)
    {
        if (group.enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned in one of the groups!");
            yield break;
        }
        
        Debug.Log($"Spawning group: {group.count}x {group.enemyPrefab.name}");
        for (int i = 0; i < group.count; i++)
        {
            Transform spawnPoint = spawnSpots[Random.Range(0, spawnSpots.Count)];
            
            GameObject newEnemy = Instantiate(group.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            activeEnemies.Add(newEnemy);
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
         

            if (group.spawnInterval > 0)
            {
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }
    }

    public void OnEnemyDefeated(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }
}