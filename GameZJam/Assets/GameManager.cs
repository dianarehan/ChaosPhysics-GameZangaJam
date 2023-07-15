using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject losePanel;
    public GameObject winPanel;
    public int nextLevelIndex =2;

    private void Start()
    {
        // Hide the lose panel and win panel at the start of the game
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void LoseGame()
    {
        // Display the lose panel when the player loses
        losePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game (optional)
    }

    public void WinGame()
    {
        // Display the win panel when the player wins
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game (optional)
    }

    public void MoveToNextLevel()
    {
        // Move to the next level by loading the scene with the next level index
        Time.timeScale = 1f; // Resume the game (optional)
        SceneManager.LoadScene(nextLevelIndex);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        // Restart the game by reloading the current scene
        Time.timeScale = 1f; // Resume the game (optional)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
