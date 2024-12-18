using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Drag your TextMeshPro UI element here
    public TimerManager timerManager;    // Reference to the TimerManager
    public EnemySpawner enemySpawner;   // Reference to the EnemySpawner

    public float countdownDuration = 3f; // Duration of the countdown

    private bool gameStarted = false; // Tracks if the game has started

    void Start()
    {
        // Freeze game at the start
        Time.timeScale = 0f;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        // Count down from the specified duration to 0
        float countdown = countdownDuration;

        while (countdown > 0)
        {
            countdownText.text = Mathf.Ceil(countdown).ToString(); // Update the TextMeshPro UI
            yield return new WaitForSecondsRealtime(1f); // Wait 1 second (unaffected by Time.timeScale)
            countdown--;
        }

        // Display "GO!" at the end
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f); // Show "GO!" for 1 second

        // Clear the countdown text
        countdownText.text = "";

        // Unfreeze the game and start necessary components
        Time.timeScale = 1f;
        gameStarted = true;

        // Start the timer and delay enemy spawning
        if (timerManager != null)
        {
            timerManager.StartTimer();
        }

        if (enemySpawner != null)
        {
            yield return new WaitForSeconds(3f); // Delay enemy spawning by 3 seconds
            enemySpawner.StartSpawning();
        }
    }
}