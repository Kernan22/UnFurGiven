using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI detectControllerPrompt; 
    public Button startGameButton; // The "Start Game" button

    private bool controllerDetected = false;

    void Start()
    {
        
        startGameButton.interactable = false;
        detectControllerPrompt.gameObject.SetActive(true);
    }

    void Update()
    {
        
        if (!controllerDetected && Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            ActivateControllers();
        }
    }

    private void ActivateControllers()
    {
        controllerDetected = true;
        
        startGameButton.interactable = true;
        detectControllerPrompt.gameObject.SetActive(false);

        Debug.Log("Controllers activated, Start Game button enabled.");
    }
}
