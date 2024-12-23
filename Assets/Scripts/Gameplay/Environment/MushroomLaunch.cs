using UnityEngine;

/// MushroomLaunch propels the player upwards when they enter the trigger zone, adjusting force based on the player's size if affected by a power-up.

public class MushroomLaunch : MonoBehaviour
{
    [Header("Launch Settings")]
    public float launchForce = 10f;  // Base launch force for the player
    public AudioClip launchSound;    // Sound effect for the launch
    public GameObject mushroomEffectPrefab;  // Visual effect for the launch

    private AudioSource audioSource; // Audio source to play sound effects

    private void Start()
    {
        // Get or add an AudioSource to play the launch sound
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Determine if the object has a PlayerController script (i.e., it's the player)
            PlayerController playerController = other.GetComponent<PlayerController>();
            float adjustedLaunchForce = launchForce;

            // If the player is scaled, adjust the launch force proportionally
            if (playerController != null)
            {
                float scaleFactor = other.transform.localScale.y / playerController.originalScale.y;
                adjustedLaunchForce *= scaleFactor;  // Scale the launch force by the player's size
            }

            // Apply upward force to the Rigidbody
            rb.AddForce(Vector3.up * adjustedLaunchForce, ForceMode.Impulse);

            // Play launch sound effect
            if (audioSource != null && launchSound != null)
            {
                audioSource.PlayOneShot(launchSound);
            }

            // Instantiate the visual effect at the mushroom's position
            if (mushroomEffectPrefab != null)
            {
                Instantiate(mushroomEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
