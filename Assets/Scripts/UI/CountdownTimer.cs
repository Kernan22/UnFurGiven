using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Import TextMeshPro namespace

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 3; // Number of seconds to count down
    public TextMeshProUGUI countdownDisplay; // Use TextMeshProUGUI for TextMeshPro

    public GameObject player1; // Reference to Player 1 GameObject
    public GameObject player2; // Reference to Player 2 GameObject

    public PowerupSpawner powerupSpawner; // Reference to the PowerupSpawner

    private Rigidbody player1Rb;
    private Rigidbody player2Rb;

    private void Start()
    {
        // Get the Rigidbody components for both players
        player1Rb = player1.GetComponent<Rigidbody>();
        player2Rb = player2.GetComponent<Rigidbody>();

        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        // Disable player controls and freeze physics at the start
        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = false;
        player1Rb.isKinematic = true;
        player2Rb.isKinematic = true;

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString(); // Show the countdown
            yield return new WaitForSeconds(1f); // Wait for one second
            countdownTime--; // Decrease the countdown
        }

        countdownDisplay.text = "GO!"; // Display "GO!" at the end of the countdown
        yield return new WaitForSeconds(1f); // Show "GO!" for a short time
        countdownDisplay.gameObject.SetActive(false); // Hide the countdown display

        // Enable player controls and unfreeze physics after the countdown
        player1.GetComponent<PlayerController>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = true;
        player1Rb.isKinematic = false;
        player2Rb.isKinematic = false;

        // Start the game timer after the countdown
        FindObjectOfType<GameTimer>().StartGameTimer();

        // Start the power-up spawner after the countdown
        powerupSpawner.StartSpawning(); 
    }
}
