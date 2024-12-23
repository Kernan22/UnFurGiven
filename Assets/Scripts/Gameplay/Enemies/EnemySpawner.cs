using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
    public Transform[] spawnPoints;   // Array of potential spawn points
    public float spawnInterval = 5f;  // Time interval between enemy spawns
    public int maxEnemies = 10;       // Max number of enemies allowed on the map

    private List<GameObject> activeEnemies = new List<GameObject>(); // List to track active enemies
    private bool isSpawning = false;  

    
    // Starts enemy spawning
    
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    // Make sure enemies spawn at the correct intervals, and as long as the limit hasn't been reached
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Spawn only if the number of active enemies is below the maximum limit
            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    public void SpawnEnemy()
    {
        // Select a random spawn point from the array
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Select a random enemy prefab to spawn
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instantiate the selected enemy at the chosen spawn point
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Add the newly spawned enemy to the active enemy list
        activeEnemies.Add(enemy);

        // Handle enemy destruction and remove it from the list when destroyed
        enemy.GetComponent<EnemyBehavior>().OnEnemyDestroyed += () => activeEnemies.Remove(enemy);
    }
}
