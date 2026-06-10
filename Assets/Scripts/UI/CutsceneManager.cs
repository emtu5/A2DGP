using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> panels;
    [SerializeField] private List<string> dialogue;
    [SerializeField] private Image panel;
    [SerializeField] private TMP_Text line;

    private int currentPanel = 0;

    public void NextPanel()
    {
        currentPanel++;
        if (currentPanel < panels.Count)
        {
            panel.sprite = panels[currentPanel];
            line.text = dialogue[currentPanel];
        }
        else if (currentPanel == panels.Count)
            LoadTutorial();
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void Start()
    {
        panel.sprite = panels[currentPanel];
        line.text = dialogue[currentPanel];
    }
}
