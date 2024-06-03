using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class InAppShop : MonoBehaviour
{
    [SerializeField] private  List<InAppShopItem> items;


    private void Start()
    {
        SubscribeAllRewards();

        Geekplay.Instance.SetPurchasedItem();
    }

    private void SubscribeAllRewards()
    {
        foreach (var item in items) 
        {
            item.SubscribeReward();
        }
    }
}
