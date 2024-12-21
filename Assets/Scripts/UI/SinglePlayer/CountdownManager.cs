using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Text for displaying the countdown
    public float countdownDuration = 3f; // Duration of the countdown
    public EnemySpawner enemySpawner; // Reference to the EnemySpawner script
    public TimerManager timerManager; // Reference to the TimerManager script
    public AudioSource backgroundMusic; // Reference to the AudioSource for music

    private bool musicStarted = false; // Ensure music starts only once

    private void Start()
    {
        // Ensure music does not start playing immediately
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop(); // Stop music if it was set to play on Awake
        }

        // Freeze the game at the start
        Time.timeScale = 0f;

        // Start the countdown
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        float countdown = countdownDuration;

        // Display countdown
        while (countdown > 0)
        {
            countdownText.text = Mathf.Ceil(countdown).ToString();
            yield return new WaitForSecondsRealtime(1f); // Wait 1 second (unaffected by Time.timeScale)
            countdown--;
        }

        // Display "GO!" at the end
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        // Clear the text
        countdownText.text = "";

        // Unfreeze the game
        Time.timeScale = 1f;

        // Enable the enemy spawning logic
        if (enemySpawner != null)
        {
            enemySpawner.InvokeRepeating(nameof(enemySpawner.SpawnEnemy), 0f, 2f); // Start spawning enemies
        }

        // Start the timer
        if (timerManager != null)
        {
            timerManager.StartTimer();
        }

        // Start background music if not already playing
        if (backgroundMusic != null && !musicStarted)
        {
            backgroundMusic.Play();
            musicStarted = true; // Mark music as started
        }
    }
}
