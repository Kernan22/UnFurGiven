using UnityEngine;
using System.Collections;

public class SinglePlayerPowerup : MonoBehaviour
{
    public GameObject powerupPrefab; // Assign the Powerup prefab in the Inspector
    public Transform[] spawnPlanes;  // Assign the planes for powerup spawning
    public float spawnInterval = 30f; // 30 seconds interval for spawning

    private GameObject currentPowerup;  // Tracks the current powerup in the scene
    private bool isPowerupActive = false;  // Tracks if the player is powered up

    void Start()
    {
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            // Only spawn if no powerup exists and player isn't powered up
            if (currentPowerup == null && !isPowerupActive)
            {
                SpawnPowerup();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPowerup()
    {
        Transform randomPlane = spawnPlanes[Random.Range(0, spawnPlanes.Length)];
        Vector3 spawnPosition = GetRandomPointInPlane(randomPlane);

        // Instantiate the powerup at the calculated position
        currentPowerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);

        // Attach a listener to handle when the powerup is picked up
        Powerup powerupScript = currentPowerup.GetComponent<Powerup>();
        if (powerupScript != null)
        {
            powerupScript.OnPickedUp += HandlePowerupPickedUp;
        }
    }

    private Vector3 GetRandomPointInPlane(Transform plane)
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError($"Plane {plane.name} is missing a Renderer component.");
            return plane.position;
        }

        Bounds bounds = planeRenderer.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        Vector3 spawnPosition = new Vector3(randomX, bounds.max.y + 5f, randomZ); // Spawn slightly above the plane

        // Raycast downward to ensure it spawns on top of the plane
        RaycastHit hit;
        if (Physics.Raycast(spawnPosition, Vector3.down, out hit, 10f))
        {
            return hit.point + Vector3.up * 0.5f;  // Slight offset to avoid clipping
        }

        // Fallback if raycast fails
        Debug.LogWarning("Raycast failed, spawning at default plane height.");
        return new Vector3(randomX, bounds.center.y + 0.5f, randomZ);  // Default to center height with slight offset
    }

    private void HandlePowerupPickedUp()
    {
        isPowerupActive = true;
        currentPowerup = null;
        StartCoroutine(PowerupCooldown());
    }

    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(10f);  // Power-up duration of 10 seconds
        isPowerupActive = false;
    }
}
