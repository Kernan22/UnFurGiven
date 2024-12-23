using UnityEngine;
using UnityEngine.SceneManagement;


// Handles Main Menu navigation, including starting the game, switching between menu screens, and quitting the application.

public class MainMenu : MonoBehaviour
{
    [Header("Menu Canvases")]
    public GameObject mainMenuCanvas;  // Reference to the main menu UI canvas
    public GameObject controlsCanvas;  // Reference to the controls/settings UI canvas
    
    // Starts the main game by loading the specified scene.
  
    public void StartGame()
    {
        // Load the default main game level (Level1)
        SceneManager.LoadScene("Level1");
    }

   
    // Starts the single-player mode by loading the SinglePlayer scene.
  
    public void StartSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
    
    // Opens the controls/settings screen by enabling the controls canvas
    public void OpenControls()
    {
        mainMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }
    
    // Returns to the main menu by disabling the controls canvas
    
    public void BackToMainMenu()
    {
        controlsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    
    // Quits the game when the Quit button is pressed.
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}