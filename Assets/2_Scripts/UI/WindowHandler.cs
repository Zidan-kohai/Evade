using UnityEngine;
public class WindowHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject ModesWindow;
    [SerializeField] private GameObject TelegramWindow;
    [SerializeField] private GameObject SimpleShopWindow;
    [SerializeField] private MainMenuWindowState currentOpenedWindow;

    #region MainMenu
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
    #endregion

    #region Telegram
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
    #endregion

    #region ModesWindow
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
    #endregion

    #region SimpleShops
    public void OpenSimpleShopWindow()
    {
        SimpleShopWindow.SetActive(true);
        CloseCurrentWindow();
        currentOpenedWindow = MainMenuWindowState.SimpleShop;
    }

    private void CloseSimpleShopWindow()
    {
        SimpleShopWindow.SetActive(false);
    }
    #endregion

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

            case MainMenuWindowState.SimpleShop:
                CloseSimpleShopWindow();
                break;
        }
    }
}
