using UnityEngine;


// Manages game over events, including stopping background music,displaying the game over panel, and handling scene transitions.

public class GameEndManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;  // Reference to the UI panel that appears on game over
    public AudioSource backgroundMusic;  // Reference to the background music AudioSource

   
    /// Triggers the game over sequence by stopping music, displaying the game over panel, and pausing the game.
    
    public void TriggerGameOver()
    {
        // Stop the background music if it is playing
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        // Show the game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Pause the game by setting time scale to zero
        Time.timeScale = 0f;
    }

    
    // Restarts the current level by reloading the scene and resuming gameplay.
    
    public void RestartGame()
    {
        // Resume the game
        Time.timeScale = 1f;

        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    
    // Quits the game and returns to the main menu by loading the MainMenu scene.
    
    public void QuitToMainMenu()
    {
        // Ensure the game resumes before loading the main menu
        Time.timeScale = 1f;

        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}