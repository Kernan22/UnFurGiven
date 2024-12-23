using UnityEngine;
using TMPro;

// Manages the player's score and updates the UI accordingly.

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the Text element for displaying the score
    private int score = 0;           // Tracks the player's current score


    // Initializes the score display when the game starts.
    
    void Start()
    {
        UpdateScoreUI(); // Set the initial score display
    }
    
    // Increments the player's score by a specified amount.
 
    public void AddScore(int points)
    {
        score += points; // Increase the score by the given amount
        UpdateScoreUI(); // Reflect the new score in the UI
    }

  
    // Updates the Text to display the current score.
        
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score; // Format and display the score in the UI
    }
}