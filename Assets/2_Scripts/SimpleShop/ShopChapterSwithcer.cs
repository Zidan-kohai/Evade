using UnityEngine;

public class ShopChapterSwithcer : MonoBehaviour
{
    [SerializeField] private RectTransform lightSubjectHandler;
    [SerializeField] private RectTransform itemSubjectHandler;
    [SerializeField] private RectTransform boosterSubjectHandler;
    [SerializeField] private RectTransform accessorySubjectHandler;

    [SerializeField] private RectTransform equipButton;
    [SerializeField] private Vector3 equipButtonOpenPosition = new Vector3(0, 200, 0);
    [SerializeField] private Vector3 equipButtonClosePosition = new Vector3(0, 15, 0);
    [SerializeField] private ShowChapter openedChapter;

    #region Light
    public void OpenLightChapter()
    {
        CloseCurrentOpenChapter();
        lightSubjectHandler.gameObject.SetActive(true);
        openedChapter = ShowChapter.Light;
    }
    private void CloseLightChapter()
    {
        lightSubjectHandler.gameObject.SetActive(false);
    }
    #endregion

    #region Item
    public void OpenItemChapter()
    {
        CloseCurrentOpenChapter();
        itemSubjectHandler.gameObject.SetActive(true);
        openedChapter = ShowChapter.Item;
    }
    private void CloseItemChapter()
    {
        itemSubjectHandler.gameObject.SetActive(false);
    }
    #endregion

    #region Booster
    public void OpenBoosterChapter()
    {
        CloseCurrentOpenChapter();
        boosterSubjectHandler.gameObject.SetActive(true);
        equipButton.anchoredPosition = equipButtonOpenPosition;
        openedChapter = ShowChapter.Booster;
    }
    private void CloseBoosterChapter()
    {
        equipButton.anchoredPosition = equipButtonClosePosition;
        boosterSubjectHandler.gameObject.SetActive(false);
    }
    #endregion

    #region Accessory
    public void OpenAccessoryChapter()
    {
        CloseCurrentOpenChapter();
        accessorySubjectHandler.gameObject.SetActive(true);
        openedChapter = ShowChapter.Accessory;
    }
    private void CloseAccessoryChapter()
    {
        accessorySubjectHandler.gameObject.SetActive(false);
    }
    #endregion


    private void CloseCurrentOpenChapter()
    {
        switch(openedChapter)
        {
            case ShowChapter.Light:
                CloseLightChapter();
                break;
            case ShowChapter.Item:
                CloseItemChapter();
                break;
            case ShowChapter.Booster:
                CloseBoosterChapter();
                break;
            case ShowChapter.Accessory:
                CloseAccessoryChapter();
                break;
        }
    }
}
