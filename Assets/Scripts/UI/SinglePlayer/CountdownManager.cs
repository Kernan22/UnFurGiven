using UnityEngine;
using TMPro;

// Manages the countdown at the start of the game, controls enemy spawning, and starts background music.

public class CountdownManager : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI countdownText;  // UI Text to display the countdown

    [Header("Countdown Settings")]
    public float countdownDuration = 3f;   // Length of the countdown before the game starts

    [Header("References")]
    public EnemySpawner enemySpawner;      // Reference to the EnemySpawner script
    public TimerManager timerManager;      // Reference to the TimerManager script
    public AudioSource backgroundMusic;    // Reference to the background music AudioSource

    private bool musicStarted = false;     // Tracks whether the background music has started to avoid overlap
    
    // Initializes the countdown and game state at the start of the scene.
    
    private void Start()
    {
        // Ensure background music is stopped at the start to avoid premature playback
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        // Freeze the game until the countdown finishes
        Time.timeScale = 0f;

        // Start the countdown coroutine
        StartCoroutine(CountdownCoroutine());
    }
    
    // Handles the countdown process and unfreezes the game afterward.
    
    private System.Collections.IEnumerator CountdownCoroutine()
    {
        float countdown = countdownDuration;

        // Countdown loop
        while (countdown > 0)
        {
            countdownText.text = Mathf.Ceil(countdown).ToString();  // Display the remaining time
            yield return new WaitForSecondsRealtime(1f);            
            countdown--;  // Decrement countdown
        }

        // Display "GO!" for 1 second after the countdown reaches zero
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        // Clear the countdown text from the screen
        countdownText.text = "";

        // Unfreeze the game by resuming the time scale
        Time.timeScale = 1f;

        // Begin enemy spawning if the spawner exists
        if (enemySpawner != null)
        {
            enemySpawner.InvokeRepeating(nameof(enemySpawner.SpawnEnemy), 0f, 2f);
        }

        // Start the round timer using TimerManager
        if (timerManager != null)
        {
            timerManager.StartTimer();
        }

        // Start playing background music if it hasn't started yet
        if (backgroundMusic != null && !musicStarted)
        {
            backgroundMusic.Play();
            musicStarted = true;  // Prevent multiple music restarts
        }
    }
}

