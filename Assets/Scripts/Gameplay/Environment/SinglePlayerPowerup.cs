using UnityEngine;
using System.Collections;

public class SinglePlayerPowerup : MonoBehaviour
{
    public GameObject powerupPrefab; // Assign the Powerup prefab in the Inspector
    public Transform[] spawnPlanes; // Assign the planes for powerup spawning
    public float spawnInterval = 30f; // 30 seconds interval for spawning

    private GameObject currentPowerup; // Tracks the current powerup in the scene
    private bool isPowerupActive = false; // Tracks if the player is powered up

    void Start()
    {
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        // Wait for the initial countdown to end
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            // Only spawn if no powerup exists in the level
            if (currentPowerup == null && !isPowerupActive)
            {
                SpawnPowerup();
            }

            // Wait for the next spawn check
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPowerup()
    {
        if (spawnPlanes.Length == 0)
        {
            Debug.LogError("No spawn planes assigned to SinglePlayerPowerup. Please assign them in the Inspector.");
            return;
        }

        // Select a random spawn plane
        Transform randomPlane = spawnPlanes[Random.Range(0, spawnPlanes.Length)];
        BoxCollider planeCollider = randomPlane.GetComponent<BoxCollider>();

        if (planeCollider == null)
        {
            Debug.LogError($"Spawn plane {randomPlane.name} is missing a BoxCollider.");
            return;
        }

        // Get a random position within the bounds of the BoxCollider
        Vector3 randomPosition = GetRandomPointInBoxCollider(planeCollider);

        if (powerupPrefab == null)
        {
            Debug.LogError("Powerup Prefab is not assigned in the SinglePlayerPowerup script.");
            return;
        }

        // Instantiate the powerup at the random position
        currentPowerup = Instantiate(powerupPrefab, randomPosition, Quaternion.identity);

        Powerup powerupScript = currentPowerup.GetComponent<Powerup>();
        if (powerupScript != null)
        {
            // Attach a listener to handle when the powerup is picked up
            powerupScript.OnPickedUp += HandlePowerupPickedUp;
        }
    }

    private Vector3 GetRandomPointInBoxCollider(BoxCollider boxCollider)
    {
        // Get the bounds of the BoxCollider
        Vector3 center = boxCollider.transform.position + boxCollider.center;
        Vector3 size = boxCollider.size;

        // Generate a random position within the bounds
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        // Use the Y position of the plane for height
        return new Vector3(randomX, center.y, randomZ);
    }

    private void HandlePowerupPickedUp()
    {
        // Mark the powerup as active and reset the 30-second timer after 10 seconds
        isPowerupActive = true;

        // Remove the reference to the current powerup
        currentPowerup = null;

        // Wait for the powerup effect to end
        StartCoroutine(PowerupCooldown());
    }

    private IEnumerator PowerupCooldown()
    {
        // Wait for 10 seconds (powerup duration)
        yield return new WaitForSeconds(10f);

        // Reset the state and allow the powerup to respawn
        isPowerupActive = false;
    }
}
