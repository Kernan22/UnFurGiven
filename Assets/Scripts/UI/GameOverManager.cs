using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject endGamePanel; // Panel with "Win" message and buttons
    public TextMeshProUGUI endGameMessage; // Text for displaying "Player X Wins" message
    public GameObject player1;
    public GameObject player2;

    public BackgroundMusicController backgroundMusicController; // Reference to the music controller

    private void Start()
    {
        endGamePanel.SetActive(false); // Hide the end game panel at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter detected: " + other.tag);

        if (other.CompareTag("Player1"))
        {
            DisplayWinner("Player 2 Wins!");
        }
        else if (other.CompareTag("Player2"))
        {
            DisplayWinner("Player 1 Wins!");
        }
    }

    private void DisplayWinner(string message)
    {
        // Stop the background music
        if (backgroundMusicController != null)
        {
            Debug.Log("Stopping music due to winner announcement...");
            backgroundMusicController.StopMusic();
        }
        else
        {
            Debug.LogWarning("BackgroundMusicController not assigned in GameOverManager!");
        }

        // Fallback: Stop all AudioSources as a safety measure
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            source.Stop();
        }

        // Display the winning message and freeze the game
        Time.timeScale = 0f;
        endGameMessage.text = message;
        endGamePanel.SetActive(true);
    }


    public void Rematch()
    {
        Time.timeScale = 1f; // Unfreeze the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Restart the scene
    }

    public void Quit()
    {
        Time.timeScale = 1f; // Unfreeze the game
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Load the main menu
    }
}