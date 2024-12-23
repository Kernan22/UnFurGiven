using UnityEngine;
using System.Collections;
using TMPro;


// Controls the countdown at the start of the game, enabling/disabling player controls, triggering animations, and starting background music when the countdown ends.

public class CountdownTimer : MonoBehaviour
{
    [Header("Countdown Settings")]
    public int countdownTime = 3;  // Time for countdown in seconds
    public TextMeshProUGUI countdownDisplay;  // TextMeshPro UI element for displaying countdown

    [Header("Player References")]
    public GameObject player1;  // Player 1 GameObject
    public GameObject player2;  // Player 2 GameObject

    [Header("Game Managers")]
    public GameOverManager gameOverManager;  // Reference to Game Over Manager
    public BackgroundMusicController backgroundMusicController;  // Music Controller for managing background music

    [Header("Animations")]
    public Animator hedgehogAnimator;  // Animator for Player 1 (Hedgehog)
    public Animator rabbitAnimator;  // Animator for Player 2 (Rabbit)

    private Rigidbody player1Rb;  // Rigidbody component for Player 1
    private Rigidbody player2Rb;  // Rigidbody component for Player 2
    
    // Initialize the countdown sequence on game start.
    
    private void Start()
    {
        InitializePlayers();

        // Ensure both player animations are disabled initially
        if (hedgehogAnimator != null)
        {
            hedgehogAnimator.enabled = false;
        }
        if (rabbitAnimator != null)
        {
            rabbitAnimator.enabled = false;
        }

        // Start the countdown coroutine
        StartCoroutine(CountdownToStart());
    }
    
    private void InitializePlayers()
    {
        player1Rb = player1.GetComponent<Rigidbody>();
        player2Rb = player2.GetComponent<Rigidbody>();
    }

    // Restart the countdown process (for game resets or restarts).
    
    public void RestartCountdown()
    {
        countdownTime = 3;  // Reset countdown time
        countdownDisplay.gameObject.SetActive(true);  // Make countdown UI visible again
        StartCoroutine(CountdownToStart());
    }

   
    // Countdown coroutine - manages the countdown, controls, and post-countdown actions.
    
    IEnumerator CountdownToStart()
    {
        // Disable player movement and controls during countdown
        DisablePlayerControls();

        // Count down from the specified time
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);  // Wait 1 second between counts
            countdownTime--;
        }

        // Display "GO!" for one second at the end of the countdown
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);

        // Hide the countdown display after "GO!"
        countdownDisplay.gameObject.SetActive(false);

        // Re-enable player movement and controls
        EnablePlayerControls();

        // Start player running animations
        if (hedgehogAnimator != null)
        {
            hedgehogAnimator.enabled = true;
            hedgehogAnimator.SetBool("isRunning", true);  // Trigger running state
        }

        if (rabbitAnimator != null)
        {
            rabbitAnimator.enabled = true;
            rabbitAnimator.SetBool("isRunning", true);  // Trigger running state
        }

        // Start background music after countdown
        if (backgroundMusicController != null)
        {
            backgroundMusicController.StartMusic();
        }

        // Start game timer
        FindObjectOfType<GameTimer>().StartGameTimer();
    }

    
    // Disables player controls and sets Rigidbody to kinematic (disables physics).
    
    private void DisablePlayerControls()
    {
        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = false;

        player1Rb.isKinematic = true;
        player2Rb.isKinematic = true;
    }
    
    // Enables player controls and reactivates Rigidbody physics.
  
    private void EnablePlayerControls()
    {
        player1.GetComponent<PlayerController>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = true;

        player1Rb.isKinematic = false;
        player2Rb.isKinematic = false;
    }
}
