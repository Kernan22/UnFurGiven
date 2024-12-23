using UnityEngine;
using System.Collections;

/// Handles the spawning of power-ups at random positions on designated planes & ensures power-ups spawn at intervals and prevents overlapping spawns.

public class SinglePlayerPowerup : MonoBehaviour
{
    [Header("Power-up Settings")]
    public GameObject powerupPrefab;     // Power-up prefab to spawn
    public Transform[] spawnPlanes;      // Planes where power-ups will spawn
    public float spawnInterval = 30f;    // Interval between spawns (in seconds)

    private GameObject currentPowerup;   // Tracks the active power-up
    private bool isPowerupActive = false; // Tracks if the player currently has a power-up

    void Start()
    {
        StartCoroutine(SpawnPowerupRoutine());
    }

   
    // Coroutine that spawns power-ups at regular intervals.
    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(spawnInterval);  // Initial delay before first spawn

        while (true)
        {
            // Spawn only if no powerup exists and the player isn't powered up
            if (currentPowerup == null && !isPowerupActive)
            {
                SpawnPowerup();
            }

            yield return new WaitForSeconds(spawnInterval);  // Wait for the next spawn interval
        }
    }

   
    // Spawns a power-up on a random plane.
    private void SpawnPowerup()
    {
        // Select a random plane from the array
        Transform randomPlane = spawnPlanes[Random.Range(0, spawnPlanes.Length)];
        
        // Calculate the spawn position on the plane
        Vector3 spawnPosition = GetRandomPointInPlane(randomPlane);

        // Instantiate the power-up at the calculated position
        currentPowerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);

        // Attach event listener to detect when power-up is picked up
        Powerup powerupScript = currentPowerup.GetComponent<Powerup>();
        if (powerupScript != null)
        {
            powerupScript.OnPickedUp += HandlePowerupPickedUp;
        }
    }
    
    // Calculates a random spawn point on the selected plane & Raycasts to ensure the power-up spawns on top of the plane.
  
    private Vector3 GetRandomPointInPlane(Transform plane)
    {
        // Ensure the plane has a Renderer to calculate bounds
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError($"Plane {plane.name} is missing a Renderer component.");
            return plane.position;  // Fallback to plane center
        }

        // Get plane bounds
        Bounds bounds = planeRenderer.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // Start the spawn slightly above the plane
        Vector3 spawnPosition = new Vector3(randomX, bounds.max.y + 5f, randomZ);

        // Raycast down to find the plane surface
        if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hit, 10f))
        {
            return hit.point + Vector3.up * 0.5f;  // Spawn slightly above the hit point to prevent clipping
        }

        // Fallback if raycast fails (spawns at center height of the plane)
        Debug.LogWarning("Raycast failed, spawning at default plane height.");
        return new Vector3(randomX, bounds.center.y + 0.5f, randomZ);
    }

  
    // Prevents new power-ups from spawning during the active power-up duration.
    private void HandlePowerupPickedUp()
    {
        isPowerupActive = true;
        currentPowerup = null;  // Clear reference to the power-up

        // Start cooldown to allow spawning after power-up duration
        StartCoroutine(PowerupCooldown());
    }
    
    // Cooldown routine that resets the power-up state after 10 seconds.
    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(10f);  // Duration of power-up
        isPowerupActive = false;
    }
}
