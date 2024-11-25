using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 3;
    public TextMeshProUGUI countdownDisplay;

    public GameObject player1;
    public GameObject player2;

    public GameOverManager gameOverManager;
    public BackgroundMusicController backgroundMusicController;

    private Rigidbody player1Rb;
    private Rigidbody player2Rb;

    public Animator hedgehogAnimator; // Reference to hedgehog's animator
    public Animator rabbitAnimator; // Reference to rabbit's animator

    private void Start()
    {
        InitializePlayers();

        // Ensure both animators are disabled initially
        if (hedgehogAnimator != null)
        {
            hedgehogAnimator.enabled = false;
        }
        if (rabbitAnimator != null)
        {
            rabbitAnimator.enabled = false;
        }

        StartCoroutine(CountdownToStart());
    }

    private void InitializePlayers()
    {
        player1Rb = player1.GetComponent<Rigidbody>();
        player2Rb = player2.GetComponent<Rigidbody>();
    }

    public void RestartCountdown()
    {
        countdownTime = 3; // Reset the countdown time
        countdownDisplay.gameObject.SetActive(true); // Show the countdown UI again
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        DisablePlayerControls();

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);

        EnablePlayerControls();

        // Start the hedgehog and rabbit animations after the countdown
        if (hedgehogAnimator != null)
        {
            hedgehogAnimator.enabled = true;
            hedgehogAnimator.SetBool("isRunning", true); // Trigger the running animation
        }

        if (rabbitAnimator != null)
        {
            rabbitAnimator.enabled = true;
            rabbitAnimator.SetBool("isRunning", true); // Trigger the running animation
        }

        // Start the background music after the countdown
        if (backgroundMusicController != null)
        {
            backgroundMusicController.StartMusic();
        }

        FindObjectOfType<GameTimer>().StartGameTimer();
    }

    private void DisablePlayerControls()
    {
        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = false;
        player1Rb.isKinematic = true;
        player2Rb.isKinematic = true;
    }

    private void EnablePlayerControls()
    {
        player1.GetComponent<PlayerController>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = true;
        player1Rb.isKinematic = false;
        player2Rb.isKinematic = false;
    }
}
