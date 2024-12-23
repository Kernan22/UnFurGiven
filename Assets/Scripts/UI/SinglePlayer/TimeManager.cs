using UnityEngine;
using TMPro;

// Manages an in-game timer that tracks elapsed time and updates a UI element.

public class TimerManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI timerText; // Reference to the Text that displays the timer

    [Header("Timer Settings")]
    private float elapsedTime = 0f;   // Tracks how long the player has been alive or active
    private bool isTimerRunning = false; // Flag to determine if the timer is active
    
    // Updates the timer every frame if the timer is running.
  
    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time by the time passed since last frame
            UpdateTimerUI(); // Refresh the timer display
        }
    }
   
    // Starts or restarts the timer by resetting elapsed time and enabling the timer.
   
    public void StartTimer()
    {
        elapsedTime = 0f; // Reset the timer to start from zero
        isTimerRunning = true; // Enable the timer to start counting
    }

    
    // Stops the timer from counting further.
  
    public void StopTimer()
    {
        isTimerRunning = false; // Disable the timer
    }

  
    // Updates the timer display by formatting elapsed time into minutes and seconds.
 
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Calculate the number of full minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Calculate remaining seconds

        // Update the timer UI with the formatted time (MM:SS)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}