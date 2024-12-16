using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 30f; // Timer for each round
    public TextMeshProUGUI timerDisplay; // UI element to display the timer
    public GameOverManager gameOverManager; // Reference to the GameOverManager

    private bool gameHasStarted = false;

    private void Update()
    {
        if (gameHasStarted && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerDisplay.text = Mathf.Ceil(timeRemaining).ToString();
        }
        else if (gameHasStarted && timeRemaining <= 0)
        {
            gameHasStarted = false;
            HandleRoundDraw();
        }
    }

    public void StartGameTimer()
    {
        timeRemaining = 30f; // Reset the timer for each round
        gameHasStarted = true;
    }

    public void ResetTimer()
    {
        timeRemaining = 30f; // Reset timer to its initial state
        gameHasStarted = false;
        timerDisplay.text = Mathf.Ceil(timeRemaining).ToString(); // Update the display
    }

    private void HandleRoundDraw()
    {
        Debug.Log("Timer ran out! Round is a draw.");
        if (gameOverManager != null)
        {
            gameOverManager.HandleRoundDraw(); // Notify the GameOverManager of a draw
        }
        else
        {
            Debug.LogError("GameOverManager is not assigned in GameTimer!");
        }
    }
}