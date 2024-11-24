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

    private Rigidbody player1Rb;
    private Rigidbody player2Rb;

    private void Start()
    {
        InitializePlayers();
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
        countdownDisplay.gameObject.SetActive(true); // Show the countdown UI
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