using UnityEngine;
using TMPro;

// Controls the game timer for each round, displays the countdown, and triggers events when time runs out.

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 30f;  // Timer duration for each round
    public TextMeshProUGUI timerDisplay;  // UI element to display the countdown timer
    public GameOverManager gameOverManager;  // Reference to the GameOverManager to handle round end

    private bool gameHasStarted = false;  // Tracks if the timer is actively counting down
    
    // Called once per frame. Updates the timer and handles round draw if time reaches zero.
    
    private void Update()
    {
        // If the game is running and the timer is active
        if (gameHasStarted && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;  // Countdown in real-time
            timerDisplay.text = Mathf.Ceil(timeRemaining).ToString();  // Update UI with remaining time
        }
        // If time runs out and the game is active
        else if (gameHasStarted && timeRemaining <= 0)
        {
            gameHasStarted = false;  // Stop the timer
            HandleRoundDraw();  // Handle round end as a draw
        }
    }

    
    // Starts or restarts the game timer for a new round.
   
    public void StartGameTimer()
    {
        timeRemaining = 30f;  // Reset the timer to the starting value
        gameHasStarted = true;  // Activate the timer
    }

    
    // Resets the timer to its initial state and stops the countdown.
  
    public void ResetTimer()
    {
        timeRemaining = 30f;  // Reset the timer
        gameHasStarted = false;  // Pause the timer
        timerDisplay.text = Mathf.Ceil(timeRemaining).ToString();  // Update the UI immediately
    }

   
    // Handles the event when the timer reaches zero, notifying the GameOverManager.
  
    private void HandleRoundDraw()
    {
        Debug.Log("Timer ran out! Round is a draw.");  // Log message for debugging

        // Notify the GameOverManager if assigned
        if (gameOverManager != null)
        {
            gameOverManager.HandleRoundDraw();
        }
        else
        {
            Debug.LogError("GameOverManager is not assigned in GameTimer!");  // Log error if missing
        }
    }
}
