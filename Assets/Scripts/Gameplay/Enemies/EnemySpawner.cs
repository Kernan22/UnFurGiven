using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints;  // Array of spawn points
    public float spawnInterval = 5f; // Time between enemy spawns

    private bool canSpawn = false;   // Control when spawning starts

    void Start()
    {
        StartCoroutine(StartSpawningAfterDelay());
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        // Wait for the countdown to finish (replace with the CountdownManager duration if needed)
        yield return new WaitForSeconds(3f + 3f); // 3-second countdown + 3-second delay after "GO!"

        // Enable spawning
        canSpawn = true;

        // Begin spawning loop
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Wait for the next spawn
        }
    }

    private void SpawnEnemy()
    {
        // Randomly select a spawn point and an enemy prefab
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instantiate the enemy
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}