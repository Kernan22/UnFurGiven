using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 30f; // Time in seconds
    public TextMeshProUGUI timerDisplay; // Text element to display the timer
    public GameObject endGamePanel; // Panel with "Draw" message and buttons
    public TextMeshProUGUI endGameMessage; // Text for displaying "Draw" message

    private bool isGameEnded = false;
    private bool gameHasStarted = false; //Flag to track if game has started

    private void Start()
    {
        endGamePanel.SetActive(false); // Hide the end game panel at the start
    }

    private void Update()
    {
        if (gameHasStarted && !isGameEnded)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Countdown the timer
                timerDisplay.text = Mathf.Ceil(timeRemaining).ToString(); // Update display
            }
            else
            {
                EndGame();
            }
        }
    }

    // Starting the game timer after the countdown ends
    public void StartGameTimer()
    {
        gameHasStarted = true;
    }

    private void EndGame()
    {
        isGameEnded = true;
        Time.timeScale = 0f; // Freeze the game
        endGamePanel.SetActive(true); // Show the end game panel
        endGameMessage.text = "Draw!"; // Display "Draw" message
    }

    public void Rematch()
    {
        Time.timeScale = 1f; // Unfreeze the game
        // Reset the scene or reload it to start a new match
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Time.timeScale = 1f; // Unfreeze the game
        // Load the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}