using UnityEngine;

// Controls the background music in the game, allowing it to start and stop as needed.

public class BackgroundMusicController : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip backgroundMusic;  // Audio clip for the background music
    [Range(0f, 1f)] public float volume = 0.5f;  // Volume control for the background music

    private AudioSource audioSource;  // Audio source component to play the music
    
    private void Awake()
    {
        // Try to get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource exists, add one dynamically
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.clip = backgroundMusic;   // Assign the selected background music clip
        audioSource.loop = true;              // Ensure the music loops continuously
        audioSource.volume = volume;          // Set the volume
        audioSource.playOnAwake = false;      // Music won't play automatically at the start
    }

    
    // Starts playing the background music if it is not already playing.
    
    public void StartMusic()
    {
        // Check if the audio source and music clip exist, and if the music isn't already playing
        if (audioSource != null && backgroundMusic != null && !audioSource.isPlaying)
        {
            audioSource.Play();  // Play the music
        }
    }
    
    // Stops the background music if it is currently playing.
  
    public void StopMusic()
    {
        // Stop the music if it is playing
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
