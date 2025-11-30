using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    private bool isGameEnded = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameOverPanel?.SetActive(false);
        victoryPanel?.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Time.timeScale = 0f;
        gameOverPanel?.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TriggerVictory()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Time.timeScale = 0f;
        victoryPanel?.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
