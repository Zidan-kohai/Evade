using UnityEngine;
public class WindowHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject ModesWindow;
    [SerializeField] private GameObject TelegramWindow;
    [SerializeField] private MainMenuWindowState currentOpenedWindow;

    public void OpenMainMenuWindow()
    {
        mainWindow.SetActive(true);
        CloseCurrentWindow();

        currentOpenedWindow = MainMenuWindowState.MainMenu;
    }

    private void CloseMainMenuWindow()
    {
        mainWindow.SetActive(false);
    }

    public void OpenTelegramWindow()
    {
        TelegramWindow.SetActive(true);
        CloseCurrentWindow();
        currentOpenedWindow = MainMenuWindowState.Telegram;
    }

    private void CloseTelegramWindow()
    {
        TelegramWindow.SetActive(false);
    }

    public void OpenModesWindow()
    {
        ModesWindow.SetActive(true);
        CloseCurrentWindow(); 
        currentOpenedWindow = MainMenuWindowState.Modes;
    }

    private void CloseModesWindow()
    {
        ModesWindow.SetActive(false);
    }

    private void CloseCurrentWindow()
    {
        switch(currentOpenedWindow)
        {
            case MainMenuWindowState.MainMenu:
                CloseMainMenuWindow();
                break;

            case MainMenuWindowState.Modes:
                CloseModesWindow();
                break;

            case MainMenuWindowState.Telegram:
                CloseTelegramWindow();
                break;
        }
    }
}
