using UnityEngine;


// Handles the controlled drop of a power-up object from its initial spawn point to the ground.

public class PowerupDropper : MonoBehaviour
{
    public float dropDistance = 1f;     // The default distance to drop if no ground is detected
    public float dropDuration = 1f;     // Time in seconds for the power-up to reach the ground

    private Vector3 targetPosition;     // The final position where the power-up will land
    private Vector3 startPosition;      // The initial spawn position of the power-up
    private float elapsedTime = 0f;     // Tracks the time elapsed during the drop
    private bool isDropping = false;    // Flags whether the power-up is currently dropping


    // Called when the power-up is instantiated.
    
    void Start()
    {
        // Store the initial spawn position
        startPosition = transform.position;

        // Raycast downwards to detect the ground and calculate the target position
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f))
        {
            targetPosition = hit.point;  // Set the target to the point where the ray hits the ground
        }
        else
        {
            // If no ground is detected, drop the power-up by the default drop distance
            targetPosition = startPosition + Vector3.down * dropDistance;
        }

        // Start the coroutine to perform the drop
        StartCoroutine(DropPowerup());
    }

    
    // Gradually drops the power-up to the target position over the specified duration.
    
    private System.Collections.IEnumerator DropPowerup()
    {
        isDropping = true;

        // Continue dropping the power-up until the elapsed time reaches the drop duration
        while (elapsedTime < dropDuration)
        {
            // Linearly interpolate between start and target positions based on elapsed time
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dropDuration);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Snap the power-up to the exact target position after the drop completes
        transform.position = targetPosition;
        isDropping = false;
    }
}
