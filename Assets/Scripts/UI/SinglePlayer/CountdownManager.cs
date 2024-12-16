using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Drag your TextMeshPro UI element here
    public float countdownDuration = 3f; // Duration of the countdown

    private bool gameStarted = false; // Tracks if the game has started

    void Start()
    {
        // Freeze game at the start
        Time.timeScale = 0f;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        // Count down from the specified duration to 0
        float countdown = countdownDuration;

        while (countdown > 0)
        {
            countdownText.text = Mathf.Ceil(countdown).ToString(); // Update the TextMeshPro UI
            yield return new WaitForSecondsRealtime(1f); // Wait 1 second (unaffected by Time.timeScale)
            countdown--;
        }

        // Display "GO!" at the end
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f); // Show "GO!" for 1 second

        // Clear the countdown text
        countdownText.text = "";

        // Unfreeze the game
        Time.timeScale = 1f;
        gameStarted = true;
    }
}