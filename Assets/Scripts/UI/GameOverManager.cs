using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static int player1Score = 0; // Static variable to persist Player 1 score
    public static int player2Score = 0; // Static variable to persist Player 2 score
    public static int currentRound = 1; // Static variable to persist the current round

    public int winningScore = 2; // Number of wins required to win the match

    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;
    public TextMeshProUGUI player1ScoreDisplay;
    public TextMeshProUGUI player2ScoreDisplay;
    public TextMeshProUGUI roundDisplay;

    private bool isRoundActive = true;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Update UI with the current scores and round
        UpdateUI();

        // Hide the end game panel at the start
        endGamePanel.SetActive(false);
        isRoundActive = true;
    }

    public void PlayerFallsInWater(string playerTag)
    {
        if (!isRoundActive) return;

        isRoundActive = false;

        if (playerTag == "Player1")
        {
            player2Score++;
            Debug.Log("Player 1 fell into water. Player 2 scores!");
        }
        else if (playerTag == "Player2")
        {
            player1Score++;
            Debug.Log("Player 2 fell into water. Player 1 scores!");
        }

        CheckForMatchEnd();
    }

    public void HandleRoundDraw()
    {
        if (!isRoundActive) return;

        isRoundActive = false;
        Debug.Log("Round ended in a draw.");
        currentRound++; // Increment the round
        RestartScene();
    }

    private void CheckForMatchEnd()
    {
        if (player1Score >= winningScore)
        {
            EndMatch("Player 1 Wins!");
        }
        else if (player2Score >= winningScore)
        {
            EndMatch("Player 2 Wins!");
        }
        else
        {
            currentRound++; // Increment the round number
            RestartScene();
        }
    }

    private void EndMatch(string message)
    {
        Time.timeScale = 0f; // Pause the game
        endGameMessage.text = message;
        endGamePanel.SetActive(true);
    }

    private void RestartScene()
    {
        UpdateUI(); // Ensure UI is up-to-date before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    private void UpdateUI()
    {
        Debug.Log($"Updating UI: Player 1 Score: {player1Score}, Player 2 Score: {player2Score}, Round: {currentRound}");
        player1ScoreDisplay.text = "Player 1 Score: " + player1Score;
        player2ScoreDisplay.text = "Player 2 Score: " + player2Score;
        roundDisplay.text = "Round " + currentRound;
    }

    public void Rematch()
    {
        // Reset scores and rounds for a new match
        player1Score = 0;
        player2Score = 0;
        currentRound = 1;

        // Reload the scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        // Reset static variables for a fresh start
        player1Score = 0;
        player2Score = 0;
        currentRound = 1;

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
