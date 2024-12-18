using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign the Game Over panel in the inspector

    void Start()
    {
        // Ensure the Game Over panel is hidden at the start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TriggerGameOver()
    {
        // Show the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            // Optional: Freeze the game
            Time.timeScale = 0f;
        }
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Go to the main menu by loading the MainMenu scene
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your main menu scene name
    }
}