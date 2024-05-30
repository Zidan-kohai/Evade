using GeekplaySchool;
using System;
using TMPro;
using UnityEngine;

public class GameplayMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject exerciseHandler;
    [SerializeField] private TextMeshProUGUI lostTimeView;
    [SerializeField] private GameObject mobilePanel;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private ActionHandler PCActionHandler;
    [SerializeField] private ActionHandler mobileActionHandler;


    private void Start()
    {
        if (Geekplay.Instance.PlayerData.CurrentEquipedItemID == 2)
        {
            exerciseHandler.SetActive(true);
        }

        if(Geekplay.Instance.mobile)
        {
            mobilePanel.SetActive(true);
            playerController.actionUI = mobileActionHandler;
        }
        else
        {
            mobilePanel.SetActive(false);
            playerController.actionUI = PCActionHandler;
        }
    }

    public void ChangeLostedTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        lostTimeView.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void Disable() 
    {
        mobilePanel.SetActive(false);
        gameObject.SetActive(false);
    }

}
