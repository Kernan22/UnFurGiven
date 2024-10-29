using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Force the Input System to recognize connected controllers at the start of the level
        InputSystem.ResetHaptics();
        InputSystem.Update();

        Debug.Log("Input system initialized for WebGL, controllers should be recognized.");
    }

    public void RestartGame()
    {
        // Reload the current scene for a rematch or reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
