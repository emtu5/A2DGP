using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        GameEvents.OnPlayPressed += LoadGame;
        GameEvents.OnQuitPressed += QuitGame;
        GameEvents.OnMainMenu += MainMenu;
    }

    void OnDisable()
    {
        GameEvents.OnPlayPressed -= LoadGame;
        GameEvents.OnQuitPressed -= QuitGame;
        GameEvents.OnMainMenu += MainMenu;
    }

    void LoadGame()
    {
        SceneManager.LoadScene("ArenaTest");
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}