using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab; // Reference to the power-up prefab
    public float spawnInterval = 15f; // Interval between spawns in seconds
    public GameObject spawnPlane; // Reference to the invisible plane above the island

    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        Debug.Log("Starting Powerup Spawn Routine"); // Debug log to confirm coroutine starts

        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            Debug.Log("Attempting to spawn power-up"); // Debug log to confirm spawning attempt

            Collider planeCollider = spawnPlane.GetComponent<Collider>();
            Vector3 spawnPosition = GetRandomPointInBounds(planeCollider.bounds);

            // Debug log to confirm spawn position
            Debug.Log("Spawning power-up at position: " + spawnPosition);

            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.center.y; // Spawn at the y-position of the plane
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }

}
