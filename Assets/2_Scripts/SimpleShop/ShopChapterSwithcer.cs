using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopChapterSwithcer : MonoBehaviour
{
    [SerializeField] private RectTransform lightSubjectHandler;
    [SerializeField] private Image lightSubjectChapterButtonImage;
    [SerializeField] private RectTransform itemSubjectHandler;
    [SerializeField] private Image itemSubjectChapterButtonImage;
    [SerializeField] private RectTransform boosterSubjectHandler;
    [SerializeField] private Image boosterSubjectChapterButtonImage;
    [SerializeField] private RectTransform accessorySubjectHandler;
    [SerializeField] private Image accessorySubjectChapterButtonImage;

    [SerializeField] private RectTransform equipButton;
    [SerializeField] private Vector3 equipButtonOpenPosition = new Vector3(0, 200, 0);
    [SerializeField] private Vector3 equipButtonClosePosition = new Vector3(0, 15, 0);
    [SerializeField] private ShowChapter openedChapter;

    [SerializeField] private Sprite openChapterSprite;
    [SerializeField] private Sprite closeChapterSprite;

    #region Light
    public void OpenLightChapter()
    {
        CloseCurrentOpenChapter();
        lightSubjectHandler.gameObject.SetActive(true);
        lightSubjectChapterButtonImage.sprite = openChapterSprite;
        openedChapter = ShowChapter.Light;
    }
    private void CloseLightChapter()
    {
        lightSubjectChapterButtonImage.sprite = closeChapterSprite;
        lightSubjectHandler.gameObject.SetActive(false);
    }
    #endregion

    #region Item
    public void OpenItemChapter()
    {
        CloseCurrentOpenChapter();
        itemSubjectChapterButtonImage.sprite = openChapterSprite;
        itemSubjectHandler.gameObject.SetActive(true);
        openedChapter = ShowChapter.Item;
    }
    private void CloseItemChapter()
    {
        itemSubjectChapterButtonImage.sprite = closeChapterSprite;
        itemSubjectHandler.gameObject.SetActive(false);
    }
    #endregion

    #region Booster
    public void OpenBoosterChapter()
    {
        CloseCurrentOpenChapter();
        boosterSubjectChapterButtonImage.sprite = openChapterSprite;
        boosterSubjectHandler.gameObject.SetActive(true);
        equipButton.anchoredPosition = equipButtonOpenPosition;
        openedChapter = ShowChapter.Booster;
    }
    private void CloseBoosterChapter()
    {
        boosterSubjectChapterButtonImage.sprite = closeChapterSprite;
        equipButton.anchoredPosition = equipButtonClosePosition;
        boosterSubjectHandler.gameObject.SetActive(false);
    }
    #endregion

    #region Accessory
    public void OpenAccessoryChapter()
    {
        CloseCurrentOpenChapter();
        accessorySubjectChapterButtonImage.sprite = openChapterSprite;
        accessorySubjectHandler.gameObject.SetActive(true);
        openedChapter = ShowChapter.Accessory;
    }
    private void CloseAccessoryChapter()
    {
        accessorySubjectChapterButtonImage.sprite = closeChapterSprite;
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
