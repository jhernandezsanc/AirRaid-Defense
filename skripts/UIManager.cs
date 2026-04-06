using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Manages the user interface elements for displaying the player's health and score, and provides methods to update these UI elements based on game events
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Health UI")]
    public Slider healthBar;
    public TMP_Text healthText;

    [Header("Score UI")]
    public TMP_Text scoreText;

    void Awake() {
        Instance = this;
    }
    // Update the health bar and health text based on the current and maximum health values
    public void UpdateHealth(int currentHealth, int maxHealth) {
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (healthText != null) {
            healthText.text = "HP: " + currentHealth + " / " + maxHealth;
        }
    }
    // Update the score text based on the current score value
    public void UpdateScore(int score) {
        if (scoreText != null) {
            scoreText.text = "Score: " + score;
        }
    }
}