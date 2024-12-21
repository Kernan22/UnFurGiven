using UnityEngine;

public class PowerupDropper : MonoBehaviour
{
    public float dropDistance = 1f;     // How far down the power-up drops
    public float dropDuration = 1f;     // Time it takes to reach the ground

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isDropping = false;

    void Start()
    {
        
        startPosition = transform.position;

        // Raycast to find the ground below
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f))
        {
            targetPosition = hit.point;
        }
        else
        {
            targetPosition = startPosition + Vector3.down * dropDistance;
        }

        StartCoroutine(DropPowerup());
    }

    private System.Collections.IEnumerator DropPowerup()
    {
        isDropping = true;
        while (elapsedTime < dropDuration)
        {
            // Lerp position over time
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dropDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position is exactly at the target
        transform.position = targetPosition;
        isDropping = false;
    }
}
