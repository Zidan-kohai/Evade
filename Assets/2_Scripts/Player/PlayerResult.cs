using System;
using TMPro;
using UnityEngine;

[Serializable]
public class PlayerResult : MonoBehaviour
{
    public GameObject handler;
    public TextMeshProUGUI name;
    public TextMeshProUGUI helpedCount;
    public TextMeshProUGUI survivedTime;
    public TextMeshProUGUI earnedMoney;
    public TextMeshProUGUI experience;


    public void Show(string name, int helpedCount, float survivedTime, int earnedMoney, int experience)
    {
        handler.SetActive(true);
        this.name.text = name;
        this.helpedCount.text = helpedCount.ToString();
        TimeSpan timeSpan = TimeSpan.FromSeconds(survivedTime);
        this.survivedTime.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        this.earnedMoney.text = $"{earnedMoney}$";
        this.experience.text = experience.ToString();
    }

}