using System.Collections;
using UnityEngine;

// CampfireHazard slows down the player when they enter its trigger zone,plays a fire sound, and attaches a burning effect to the player.

public class CampfireHazard : MonoBehaviour
{
    [Header("Hazard Settings")]
    public float slowMultiplier = 0.5f;  // Speed reduction multiplier (e.g., 0.5 = 50% speed)
    public float slowDuration = 5f;      // Duration of the slowdown effect in seconds

    [Header("Visual & Audio Effects")]
    public GameObject fireEffectPrefab;  // Prefab for visual fire effect
    public AudioClip fireSound;          // Sound clip for fire hazard
    [Range(0f, 1f)] public float fireSoundVolume = 0.5f; // Volume control for fire sound

    private AudioSource audioSource;     // Audio source for fire loop

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure and play the fire sound loop
        audioSource.clip = fireSound;
        audioSource.loop = true;
        audioSource.spatialBlend = 1f;       // 3D sound behavior
        audioSource.maxDistance = 10f;       // Maximum distance for hearing the sound
        audioSource.volume = fireSoundVolume;
        audioSource.Play();
    }

  
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // Apply the speed reduction to the player
            player.ApplySpeedModifier(slowMultiplier, slowDuration);

            // Instantiate fire effect and attach to the player
            if (fireEffectPrefab != null)
            {
                GameObject fireEffect = Instantiate(fireEffectPrefab, player.transform.position, Quaternion.identity);
                fireEffect.transform.SetParent(player.transform); // Attach effect to player
                Destroy(fireEffect, slowDuration); // Destroy the effect after the debuff ends
            }
        }
    }
}
