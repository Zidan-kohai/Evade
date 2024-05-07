using UnityEngine;
public class WindowHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject modesWindow;
    [SerializeField] private GameObject telegramWindow;
    [SerializeField] private GameObject simpleShopWindow;
    [SerializeField] private GameObject inAPphopWindow;
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
        telegramWindow.SetActive(true);
        CloseCurrentWindow();
        currentOpenedWindow = MainMenuWindowState.Telegram;
    }

    private void CloseTelegramWindow()
    {
        telegramWindow.SetActive(false);
    }
    #endregion

    #region ModesWindow
    public void OpenModesWindow()
    {
        modesWindow.SetActive(true);
        CloseCurrentWindow(); 
        currentOpenedWindow = MainMenuWindowState.Modes;
    }

    private void CloseModesWindow()
    {
        modesWindow.SetActive(false);
    }
    #endregion

    #region SimpleShops
    public void OpenSimpleShopWindow()
    {
        simpleShopWindow.SetActive(true);
        CloseCurrentWindow();
        currentOpenedWindow = MainMenuWindowState.SimpleShop;
    }

    private void CloseSimpleShopWindow()
    {
        simpleShopWindow.SetActive(false);
    }
    #endregion

    #region InAppShop
    public void OpenInAppShopShopWindow()
    {
        inAPphopWindow.SetActive(true);
        CloseCurrentWindow();
        currentOpenedWindow = MainMenuWindowState.InAppShop;
    }

    private void CloseInAppShopShopWindow()
    {
        inAPphopWindow.SetActive(false);
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
            case MainMenuWindowState.InAppShop:
                CloseInAppShopShopWindow();
                break;
        }
    }
}
