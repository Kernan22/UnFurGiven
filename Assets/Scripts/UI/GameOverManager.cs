using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public int winningScore = 2; // Number of wins required to win the match

    public GameObject endGamePanel;
    public TextMeshProUGUI endGameMessage;
    public TextMeshProUGUI player1ScoreDisplay;
    public TextMeshProUGUI player2ScoreDisplay;
    public TextMeshProUGUI roundDisplay;

    private bool isRoundActive = true;
    private BackgroundMusicController musicController;

    private void Start()
    {
        musicController = FindObjectOfType<BackgroundMusicController>();
        InitializeGame();
    }

    private void InitializeGame()
    {
        UpdateUI();
        endGamePanel.SetActive(false);

        if (musicController != null)
        {
            musicController.StopMusic();
        }
    }

    public void PlayerFallsInWater(string playerTag)
    {
        if (!isRoundActive) return;

        isRoundActive = false;

        if (playerTag == "Player1")
        {
            GameManager.Player2Score++;
        }
        else if (playerTag == "Player2")
        {
            GameManager.Player1Score++;
        }

        CheckForMatchEnd();
    }

    public void HandleRoundDraw()
    {
        if (!isRoundActive) return;

        isRoundActive = false;
        Debug.Log("Round ended in a draw.");
        CheckForMatchEnd();
    }

    private void CheckForMatchEnd()
    {
        if (GameManager.Player1Score >= winningScore)
        {
            EndMatch("Player 1 Wins!");
        }
        else if (GameManager.Player2Score >= winningScore)
        {
            EndMatch("Player 2 Wins!");
        }
        else
        {
            GameManager.CurrentRound++;
            RestartScene();
        }
    }

    private void EndMatch(string message)
    {
        if (musicController != null)
        {
            musicController.StopMusic();
        }

        Time.timeScale = 0f; 
        endGameMessage.text = message;
        endGamePanel.SetActive(true);
    }

    private void RestartScene()
    {
        UpdateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUI()
    {
        player1ScoreDisplay.text = "Player 1 Score: " + GameManager.Player1Score;
        player2ScoreDisplay.text = "Player 2 Score: " + GameManager.Player2Score;
        roundDisplay.text = "Round " + GameManager.CurrentRound;
    }

    public void Rematch()
    {
        GameManager.ResetGameState();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        GameManager.ResetGameState();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public static class GameManager
    {
        public static int Player1Score = 0;
        public static int Player2Score = 0;
        public static int CurrentRound = 1;

        public static void ResetGameState()
        {
            Player1Score = 0;
            Player2Score = 0;
            CurrentRound = 1;
        }
    }

}
