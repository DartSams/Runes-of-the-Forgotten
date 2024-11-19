using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton instance for global access
    public static GameManager Instance { get; private set; }

    // Player properties
    public int lives { get; private set; } = 3;    // Number of lives
    public int coins { get; private set; } = 0;    // Coin count
    public int score = 0;    // Player score

    // Game conditions
    public int coinCount = 0;
    public int winThreshold = 27;      // Coins required to win
    // public int goombaKillPoints = 10;  // Points earned for defeating a Goomba

    // UI references
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverUI;  // Game Over screen

    private void Awake()
    {
        // Ensure only one instance of GameManager exists across scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist GameManager across scene loads
    }

    private void Start()
    {
        NewGame(); // Initialize a new game on start
    }

    // Method to start a new game, resetting lives, score, and coins
    public void NewGame()
    {
        score = 0;    // Reset score
        lives = 3;    // Set initial lives
        coins = 0;    // Reset coin count
        UpdateUI();   // Update UI to reflect reset values
        gameOverUI.SetActive(false); // Hide Game Over UI if visible
        LoadLevel();  // Load the first game level

    }

    // Method to handle Game Over condition
    public void GameOver(bool allCoinsCollected = false)
    {
        // Check if player is out of lives or has collected all coins
        if (lives <= 0 || allCoinsCollected)
        {
            // Ensure Game Over UI is assigned, then show it
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true); // Show Game Over panel
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                Debug.LogWarning("GameOver UI is missing or not assigned!");
            }
        }
        else
        {
            ResetLevel(); // Reset the current level if there are lives left
        }
    }

    // Load the main level
    public void LoadLevel()
    {

        Time.timeScale = 1f; // Ensure time scale is normal when reloading
        SceneManager.LoadSceneAsync(1); // Load scene 1 (main game scene)
    }

    // Delayed level reset method
    public void ResetLevel(float delay = 0f)
    {
        Invoke(nameof(ResetLevel), delay); // Invoke reset after delay
    }

    // Reset the current level, updating lives and checking for Game Over
    public void ResetLevel()
    {
        lives--; // Decrement player lives

        if (lives > 0)
        {
            UpdateUI();  // Update UI to show new life count
            LoadLevel(); // Reload the current level
        }
        else
        {
            GameOver(false);  // Trigger Game Over if out of lives
        }
    }

    // Increment coin count and check win condition
    public void AddCoin()
    {
        coins++; // Increase coin count
        score++; // Increase score
        UpdateUI(); // Update the UI with new values

        // Check if player has collected enough coins to win
        if (coins >= winThreshold)
        {
            GameOver(true); // Trigger Game Over if all coins collected
        }
    }

    // Method to add points, typically for actions like defeating enemies
    public void AddScore(int points)
    {
        score += points; // Increment score by points
        UpdateUI(); // Update the UI to reflect new score
    }

    // Update UI elements to reflect current lives and score
    private void UpdateUI()
    {
        // Update score text if UI element is assigned
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        // Update lives text if UI element is assigned
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    // Restart the game from Game Over screen
    public void Restart()
    {
        Time.timeScale = 1f; // Unpause the game if paused
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // Hide Game Over panel
        }
        NewGame(); // Start a new game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
