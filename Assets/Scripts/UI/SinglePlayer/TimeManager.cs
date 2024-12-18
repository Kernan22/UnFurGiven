using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI element for the timer
    private float elapsedTime = 0f;   // Time the player has been alive
    private bool isTimerRunning = false; // Tracks whether the timer is active

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime; // Increment the timer by the elapsed time
            UpdateTimerUI();
        }
    }

    // Starts the timer
    public void StartTimer()
    {
        elapsedTime = 0f; // Reset the timer
        isTimerRunning = true;
    }

    // Stops the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Updates the timer UI
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Calculate seconds
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the timer text
    }
}
