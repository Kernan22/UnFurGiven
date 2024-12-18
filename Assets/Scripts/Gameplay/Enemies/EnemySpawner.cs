using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints;  // Array of spawn points
    public float spawnInterval = 5f; // Time interval between spawns
    public int maxEnemies = 10;      // Maximum number of enemies on the map

    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool isSpawning = false; // Flag to control spawning

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Choose a random enemy prefab
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instantiate the enemy and add it to the active list
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(enemy);

        // Remove the enemy from the active list when destroyed
        enemy.GetComponent<EnemyBehavior>().OnEnemyDestroyed += () => activeEnemies.Remove(enemy);
    }
}