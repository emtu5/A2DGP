using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject overlay;
    public GameObject pauseButton;

    void Start()
    {
        overlay.SetActive(false);

        pauseButton.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnEnable()
    {
        PauseManager.OnPause += ShowPauseMenu;
        PauseManager.OnResume += HidePauseMenu;
    }

    void OnDisable()
    {
        PauseManager.OnPause -= ShowPauseMenu;
        PauseManager.OnResume -= HidePauseMenu;
    }

    public void Pause()
    {
        PauseManager.Instance.PauseGame();
    }

    public void Resume()
    {
        PauseManager.Instance.ResumeGame();
    }

    void ShowPauseMenu()
    {
        overlay.SetActive(true);

        pauseButton.SetActive(false);
    }

    void HidePauseMenu()
    {
        overlay.SetActive(false);

        pauseButton.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}