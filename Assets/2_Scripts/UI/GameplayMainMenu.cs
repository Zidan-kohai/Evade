using GeekplaySchool;
using System;
using TMPro;
using UnityEngine;

public class GameplayMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject exerciseHandler;
    [SerializeField] private TextMeshProUGUI lostTimeView;

    private void Start()
    {
        if (Geekplay.Instance.PlayerData.CurrentEquipedItemID == 1)
        {
            exerciseHandler.SetActive(true);
        }
    }

    public void ChangeLostedTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        lostTimeView.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void Disable() 
    {
        gameObject.SetActive(false);
    }

}
