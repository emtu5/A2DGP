using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        GameEvents.OnPlayPressed += LoadGame;
        GameEvents.OnQuitPressed += QuitGame;
    }

    void OnDisable()
    {
        GameEvents.OnPlayPressed -= LoadGame;
        GameEvents.OnQuitPressed -= QuitGame;
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Antonia");
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}