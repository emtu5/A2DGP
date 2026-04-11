using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void OnEnable()
    {
        GameEvents.OnSettingsPressed += OpenSettings;
        GameEvents.OnBackPressed += CloseSettings;
    }

    void OnDisable()
    {
        GameEvents.OnSettingsPressed -= OpenSettings;
        GameEvents.OnBackPressed -= CloseSettings;
    }

    void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}