using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas; // Assign the Main Menu canvas in the Inspector
    public GameObject controlsCanvas; // Assign the Controls canvas in the Inspector

    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("Level1");
    }

    public void StartSinglePlayer()
    {
        // Load the single-player mode scene
        SceneManager.LoadScene("SinglePlayer");
    }

    public void OpenControls()
    {
        // Switch to the Controls canvas
        mainMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        // Switch back to the Main Menu canvas
        controlsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Game Quit");
        Application.Quit();
    }
}