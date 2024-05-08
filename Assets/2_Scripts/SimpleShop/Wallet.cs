using GeekplaySchool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private static Wallet instance;

    [SerializeField] private List<TextMeshProUGUI> goldsCountView;
    [SerializeField] private List<TextMeshProUGUI> diamondsCountView;

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

    public static void AddMoneyST(int gold, int diamond)
    {
        instance.AddMoney(gold, diamond); 
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
        Geekplay.Instance.Save();
    }
    
    private void AddMoney(int gold, int diamond)
    {
        Geekplay.Instance.PlayerData.Gold += gold;
        Geekplay.Instance.PlayerData.Diamond += diamond;

        Geekplay.Instance.Save();
        ChangeView();
    }

    private void ChangeView()
    {
        foreach (var item in goldsCountView)
        {
            item.text = Geekplay.Instance.PlayerData.Gold.ToString();
        }

        foreach (var item in diamondsCountView)
        {
            item.text = Geekplay.Instance.PlayerData.Diamond.ToString();
        }
    }
}