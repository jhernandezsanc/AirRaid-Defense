using UnityEngine;
using UnityEngine.SceneManagement;

// Handles restarting the game, quitting the application, and loading the main menu from the game over screen
public class RestartGame : MonoBehaviour
{
    public void Restart() {
        //Debug.Log("Restarting game...");
        // Reset time, then reload the current scene to restart the game
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame() {
        //Debug.Log("Quitting game...");
        // Quit the application : only can be seen in a built version of the game, not in the editor
        Application.Quit();
    }

    public void LoadMenu() {
        // Load the main menu scene
        SceneManager.LoadScene("Game_Start");
    }
}