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
    private List<GameObject> activeEnemies = new List<GameObject>(); // List to track living enemies
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

        // Start the main level progression process
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

    // The main coroutine that manages the entire level
    private IEnumerator RunLevel()
    {
        Debug.Log($"Level Starting: {currentLevel.levelName}");

        // Iterate through all waves defined in the level asset
        foreach (var wave in currentLevel.waves)
        {
            // Wait for the delay before the wave starts
            if (wave.delayBeforeWave > 0)
            {
				Debug.Log($"waves in the level: {currentLevel.waves.Count}");
                yield return new WaitForSeconds(wave.delayBeforeWave);
            }
            
            Debug.Log($"Wave Starting: {wave.waveName}");
            // Start spawning the current wave
            yield return StartCoroutine(SpawnWave(wave));
            
            // Wait until all spawned enemies are defeated
            yield return new WaitForSeconds(wave.delayBeforeNextWave);
            
            Debug.Log($"Wave {wave.waveName} cleared!");
            currentWaveIndex++;
        }
        
        Debug.Log($"LEVEL {currentLevel.levelName} COMPLETE!");
        // Here you can show a victory screen or load the next level
    }

    // Coroutine to spawn a single wave
    private IEnumerator SpawnWave(Wave wave)
    {
            Debug.Log($"spawning wave: {wave.waveName} ");
        // Iterate through all enemy groups in the wave
        foreach (var group in wave.enemyGroups)
        {
            // Start spawning a specific group
			yield return StartCoroutine(SpawnEnemyGroup(group));
			
        }
    }
    
    // Coroutine to spawn a single group of enemies
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
            // Select a random spawn point
            Transform spawnPoint = spawnSpots[Random.Range(0, spawnSpots.Count)];
            
            // Create the enemy instance
            GameObject newEnemy = Instantiate(group.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Add the enemy to the active list for tracking
            activeEnemies.Add(newEnemy);
            
            // Optionally, pass a reference of the spawner to the enemy, so it can report its death
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
         

            // Wait for the interval before spawning the next enemy in the group
            if (group.spawnInterval > 0)
            {
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }
    }

    // This method should be called by enemies when they are defeated
    public void OnEnemyDefeated(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }
}