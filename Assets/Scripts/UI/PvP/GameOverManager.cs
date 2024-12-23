using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


// Manages the game-over state, tracks player scores, and controls scene transitions.

public class GameOverManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int winningScore = 2;  // Number of wins required to end the match

    [Header("UI References")]
    public GameObject endGamePanel;  // Panel displayed when the game ends
    public TextMeshProUGUI endGameMessage;  // Message displayed at the end of the game
    public TextMeshProUGUI player1ScoreDisplay;  // UI element for Player 1's score
    public TextMeshProUGUI player2ScoreDisplay;  // UI element for Player 2's score
    public TextMeshProUGUI roundDisplay;  // UI element for displaying the current round

    private bool isRoundActive = true;  // Tracks whether the current round is still ongoing
    private BackgroundMusicController musicController;  // Reference to control background music
    
    // Initializes the game state when the scene starts.
    
    private void Start()
    {
        // Find the BackgroundMusicController in the scene
        musicController = FindObjectOfType<BackgroundMusicController>();

        InitializeGame();
    }
    
    // Prepares the initial UI and game state.
   
    private void InitializeGame()
    {
        UpdateUI();
        endGamePanel.SetActive(false);  // Hide the end-game panel at the start

        // Stop music at the beginning of the round
        if (musicController != null)
        {
            musicController.StopMusic();
        }
    }

    
    /// Called when a player falls into the water & determines which player scores.
    public void PlayerFallsInWater(string playerTag)
    {
        if (!isRoundActive) return;  // Prevent multiple triggers for one round

        isRoundActive = false;

        // Update scores based on which player fell
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
    
    // Handles the round draw condition when the timer reaches zero.
   
    public void HandleRoundDraw()
    {
        if (!isRoundActive) return;  // Prevent multiple triggers for one round

        isRoundActive = false;
        Debug.Log("Round ended in a draw.");
        CheckForMatchEnd();
    }


    // Checks if the match should end or proceed to the next round.
   
    private void CheckForMatchEnd()
    {
        // Player 1 wins
        if (GameManager.Player1Score >= winningScore)
        {
            EndMatch("Player 1 Wins!");
        }
        // Player 2 wins
        else if (GameManager.Player2Score >= winningScore)
        {
            EndMatch("Player 2 Wins!");
        }
        // Continue to the next round
        else
        {
            GameManager.CurrentRound++;
            RestartScene();
        }
    }

    
    // Ends the match, stops gameplay, and displays the winner.
   
    private void EndMatch(string message)
    {
        if (musicController != null)
        {
            musicController.StopMusic();
        }

        Time.timeScale = 0f;  // Pause the game
        endGameMessage.text = message;
        endGamePanel.SetActive(true);  // Show the end-game panel
    }

 
    // Restarts the current round by reloading the scene.
  
    private void RestartScene()
    {
        UpdateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload current scene
    }
    
    // Updates the UI with the latest scores and round number.
 
    private void UpdateUI()
    {
        player1ScoreDisplay.text = "Player 1 Score: " + GameManager.Player1Score;
        player2ScoreDisplay.text = "Player 2 Score: " + GameManager.Player2Score;
        roundDisplay.text = "Round " + GameManager.CurrentRound;
    }
    
    // Starts a rematch by resetting all game values and reloading the scene.
 
    public void Rematch()
    {
        GameManager.ResetGameState();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Quits the current match and returns to the main menu.
  
    public void Quit()
    {
        GameManager.ResetGameState();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    // Manages game-wide state such as player scores and round tracking.
   
    public static class GameManager
    {
        public static int Player1Score = 0;  // Player 1's score
        public static int Player2Score = 0;  // Player 2's score
        public static int CurrentRound = 1;  // Tracks the current round

       
        // Resets all game state values to their initial defaults.
       
        public static void ResetGameState()
        {
            Player1Score = 0;
            Player2Score = 0;
            CurrentRound = 1;
        }
    }
}
