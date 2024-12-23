using UnityEngine;
using TMPro;

public class BossSpawner : MonoBehaviour
{
    [Header("UI & Prefab References")]
    public TextMeshProUGUI scoreText;  // Reference to the score TextMeshPro UI element
    public GameObject bossPrefab;      // Reference to the Boss prefab for instantiation

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;    // Potential boss spawn points

    private int lastBossSpawnScore = 0; // Score tracker

    void Update()
    {
        int currentScore = 0;

        
        if (int.TryParse(System.Text.RegularExpressions.Regex.Match(scoreText.text, @"\d+").Value, out currentScore))
        {
            // Spawn boss if score is a multiple of 15, and ensure it spawns only once per every 15 kills
            if (currentScore >= 15 && currentScore % 15 == 0 && currentScore != lastBossSpawnScore)
            {
                SpawnBoss();
                lastBossSpawnScore = currentScore; // Update the last spawn score to avoid re-triggering
            }
        }
        else
        {
            Debug.LogWarning("Score text is not in a valid format. Ensure the score is displayed as an integer.");
        }
    }

    // Boss Spawn
    private void SpawnBoss()
    {
        Transform spawnPoint = FindClosestSpawnPointToPlayer();

        if (spawnPoint != null)
        {
            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Boss Spawned at: " + spawnPoint.position);
        }
        else
        {
            Debug.LogWarning("No valid spawn point found for the boss.");
        }
    }

    
    // Finds the spawn point nearest to the player.
    private Transform FindClosestSpawnPointToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");  // Locate player by tag
        if (player == null) return null;

        Transform closest = null;
        float closestDistance = float.MaxValue;

        // Loop through all spawn points to determine the closest one
        foreach (Transform spawnPoint in spawnPoints)
        {
            float distance = Vector3.Distance(spawnPoint.position, player.transform.position);
            if (distance < closestDistance)
            {
                closest = spawnPoint;
                closestDistance = distance;
            }
        }

        return closest;
    }
}
