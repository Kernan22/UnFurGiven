using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI element
    private int score = 0;           // Player's score

    void Start()
    {
        UpdateScoreUI(); // Initialize the score UI
    }

    public void AddScore(int points)
    {
        score += points; // Increment the score
        UpdateScoreUI(); // Update the score UI
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score; // Display the updated score
    }
}