using GeekplaySchool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private static Wallet instance;

    [SerializeField] private List<TextMeshProUGUI> goldsCountView;

    private void Start()
    {
        instance = this;
        ChangeView();
    }

    public static bool TryBuyST(int cost)
    {
        return instance.TryBuy(cost);
    }

    public static void BuyST(int cost)
    {
        instance.Buy(cost);
    }

    public static void AddMoneyST(int gold)
    {
        DailyExerciseController.Instance.SetProgress(Days.Day2, 3, gold);

        instance.AddMoney(gold); 
    }

    private bool TryBuy(int cost)
    {
        if(Geekplay.Instance.PlayerData.Gold >= cost)
        {
            return true;
        }

        return false;
    }

    private void Buy(int cost)
    {
        Geekplay.Instance.PlayerData.Gold -= cost;
        ChangeView();
        DailyExerciseController.Instance.SetProgress(Days.Day3, 3,cost);

        Geekplay.Instance.Save();
    }
    
    private void AddMoney(int gold)
    {
        Geekplay.Instance.PlayerData.Gold += gold;

        Geekplay.Instance.Save();
        ChangeView();
    }

    private void ChangeView()
    {
        foreach (var item in goldsCountView)
        {
            item.text = Geekplay.Instance.PlayerData.Gold.ToString();
        }
    }
}