using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


// Controls the main menu, detecting connected controllers and enabling the "Start Game" button when a controller input is detected.

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI detectControllerPrompt;  // UI prompt to instruct the player to connect a controller
    public Button startGameButton;                  // The "Start Game" button (disabled by default)

    private bool controllerDetected = false;        // Tracks if a controller has been detected

   
    // Initializes the main menu by disabling the start button and showing the controller prompt.
    
    void Start()
    {
        // Disable the start game button until a controller is detected
        startGameButton.interactable = false;
        
        // Show the "connect controller" prompt at the start
        detectControllerPrompt.gameObject.SetActive(true);
    }
    
    // Continuously checks for controller input during each frame.
   
    void Update()
    {
        // If no controller has been detected yet, check for controller input
        if (!controllerDetected && Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            ActivateControllers();
        }
    }

    
    // Enables controller interaction once a valid input is detected.
    private void ActivateControllers()
    {
        controllerDetected = true;

        // Enable the start game button
        startGameButton.interactable = true;

        // Hide the controller detection prompt
        detectControllerPrompt.gameObject.SetActive(false);

        Debug.Log("Controllers activated, Start Game button enabled.");
    }
}