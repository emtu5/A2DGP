using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public static event Action OnPause;
    public static event Action OnResume;

    public bool isPaused = false;

    void Awake()
    {
        // singleton simplu
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused) return;

        Time.timeScale = 0f;

        isPaused = true;

        OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        Time.timeScale = 1f;

        isPaused = false;

        OnResume?.Invoke();
    }
}