using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI detectControllerPrompt; // Text to prompt the player to press X
    public Button startGameButton; // The "Start Game" button

    private bool controllerDetected = false;

    void Start()
    {
        // Initially disable the Start Game button and show the prompt
        startGameButton.interactable = false;
        detectControllerPrompt.gameObject.SetActive(true);
    }

    void Update()
    {
        // Check for X button press on any connected gamepad
        if (!controllerDetected && Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            ActivateControllers();
        }
    }

    private void ActivateControllers()
    {
        controllerDetected = true;

        // Enable the Start Game button and hide the prompt
        startGameButton.interactable = true;
        detectControllerPrompt.gameObject.SetActive(false);

        Debug.Log("Controllers activated, Start Game button enabled.");
    }
}
