using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void Play() => GameEvents.PlayPressed();
    public void Settings() => GameEvents.SettingsPressed();
    public void Back() => GameEvents.BackPressed();
    public void Quit() => GameEvents.QuitPressed();
}