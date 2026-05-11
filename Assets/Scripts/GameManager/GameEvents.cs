using System;

public static class GameEvents
{
    public static event Action OnPlayPressed;
    public static event Action OnSettingsPressed;
    public static event Action OnBackPressed;
    public static event Action OnQuitPressed;

    public static event Action OnMainMenu;

    public static void PlayPressed() => OnPlayPressed?.Invoke();
    public static void SettingsPressed() => OnSettingsPressed?.Invoke();
    public static void BackPressed() => OnBackPressed?.Invoke();
    public static void QuitPressed() => OnQuitPressed?.Invoke();
    public static void MainMenu () => OnMainMenu?.Invoke();
}