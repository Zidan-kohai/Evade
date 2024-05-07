using GeekplaySchool;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private static Wallet instance;

    private void Awake()
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    public static bool TryBuyST(int cost)
    {
        return instance.TryBuy(cost);
    }

    public static void BuyST(int cost)
    {
        instance.Buy(cost);
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
        Geekplay.Instance.Save();
    }
}