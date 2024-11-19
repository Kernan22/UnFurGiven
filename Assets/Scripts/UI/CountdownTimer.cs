using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 3; // Number of seconds to count down
    public TextMeshProUGUI countdownDisplay;

    public GameObject player1;
    public GameObject player2;

    public PowerupSpawner powerupSpawner;
    public BackgroundMusicController backgroundMusicController; // Reference to the music controller

    private Rigidbody player1Rb;
    private Rigidbody player2Rb;

    private void Start()
    {
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
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);

        // Start the background music after the countdown ends
        if (backgroundMusicController != null)
        {
            backgroundMusicController.StartMusic();
        }

        // Enable player controls and unfreeze physics
        player1.GetComponent<PlayerController>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = true;
        player1Rb.isKinematic = false;
        player2Rb.isKinematic = false;

        // Start the game timer after the countdown
        FindObjectOfType<GameTimer>().StartGameTimer();

        // Delay the power-up spawner by 10 seconds after the countdown
        if (powerupSpawner != null)
        {
            StartCoroutine(DelayPowerupSpawner(10f));
        }
    }

    IEnumerator DelayPowerupSpawner(float delay)
    {
        yield return new WaitForSeconds(delay);
        powerupSpawner.StartSpawning();
    }

    public void EndGame()
    {
        // Stop the background music when the game ends
        if (backgroundMusicController != null)
        {
            backgroundMusicController.StopMusic();
        }

        Debug.Log("Game ended!");
    }
}

