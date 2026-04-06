using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int baseHealth = 500;
    public int maxHealth = 500;
    public int score = 0;

    public GameObject gameOverPanel;

    public AudioClip gameOverSound;
    private AudioSource audioSource;

    
    // Initialize the singleton instance
    void Awake() {
        Instance = this;
    }
    // Initialize health, score, UI, and audio source. Also hide the game over panel at the start of the game 
    void Start() {
        audioSource = GetComponent<AudioSource>();

        if (UIManager.Instance != null) {
            // Update the UI with the initial health and score values
            UIManager.Instance.UpdateHealth(baseHealth, maxHealth);
            UIManager.Instance.UpdateScore(score);
        }
        // hides game over panel
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(false);
        }
    }

    // Public method to damage the base and check for game over condition
    public void DamageBase(int amount) {
        // Decrease the base health by the specified amount
        baseHealth -= amount;
        // Ensure that health does not drop below zero
        if (baseHealth < 0) 
            baseHealth = 0;
        // Update the health display on the UI
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealth(baseHealth, maxHealth);
        // Check if the base health has dropped to zero or below, and trigger game over if so
        if (baseHealth <= 0)
            GameOver();
    }

    // Public method to add score
    public void AddScore(int amount) {
        // Increase the score by the specified amount
        score += amount;
        // Update the score display on the UI
        if (UIManager.Instance != null) {
            UIManager.Instance.UpdateScore(score);
        }
        
        //Debug.Log("Score is now: " + score);
    }

    void GameOver() {
        //Debug.Log("Game Over");
        // Stop the game by setting time scale to 0
        Time.timeScale = 0f;
        // Fade out game music
        GameMusicFadeIn music = FindObjectOfType<GameMusicFadeIn>();
        if (music != null) {
            music.FadeOut(1f);
        }
        // Play game over sound effect
        if (audioSource != null && gameOverSound != null) {
            audioSource.PlayOneShot(gameOverSound);
        }
        // Show the game over panel
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
    }
}