using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Reference to the game over panel
    public AudioSource backgroundMusic; // Reference to the background music AudioSource

    public void TriggerGameOver()
    {
        // Stop the music when the game is over
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        // Display the game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Resume the game
        Time.timeScale = 1f;

        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        // Resume the game and load the main menu scene
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}