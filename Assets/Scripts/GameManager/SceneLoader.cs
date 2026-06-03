using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    void OnEnable()
    {
        GameEvents.OnPlayPressed += LoadGame;
        GameEvents.OnQuitPressed += QuitGame;
        GameEvents.OnMainMenu += MainMenu;
        Time.timeScale = 1f;
    }

    void OnDisable()
    {
        GameEvents.OnPlayPressed -= LoadGame;
        GameEvents.OnQuitPressed -= QuitGame;
        GameEvents.OnMainMenu += MainMenu;
    }

    void LoadGame()
    {
        SceneManager.LoadScene(sceneToLoad);
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