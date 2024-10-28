using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene 
        SceneManager.LoadScene("Level1");
    }

    public void OpenOptions()
    {
        // Display options UI (TBD)
        Debug.Log("Options Menu Opened"); // Placeholder for now
    }

    public void QuitGame()
    {
        // Quit the application (TBD)
        Debug.Log("Game Quit"); // Only logs in the editor
        Application.Quit();
    }
}
