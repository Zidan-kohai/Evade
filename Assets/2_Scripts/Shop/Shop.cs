using GeekplaySchool;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button buyButton;

    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI descriptionView;
    [SerializeField] private TextMeshProUGUI costView;

    [SerializeField] private List<ShopItem> items;
    [SerializeField] private ShopItem firstItem;

    private void Start()
    {
        FirstSubscribe(firstItem);

        SubscribeEventOnItemsClick();
    }

    private void FirstSubscribe(ShopItem item)
    {
        ShowInfo(firstItem);

        Event(item);
    }

    private void SubscribeEventOnItemsClick()
    {
        foreach (ShopItem item in items)
        {
            item.SubscribeEvent(() =>
            {
                Event(item);
            });
        }
    }

    private void Event(ShopItem item)
    {
        ShowInfo(item);

        buyButton.onClick.RemoveAllListeners();

        buyButton.onClick.AddListener(() =>
        {
            Buy(item);

        });
    }

    private void ShowInfo(ShopItem item)
    {
        nameView.text = item.GetName;
        descriptionView.text = item.GetDescription;
        costView.text = item.GetCost.ToString();
    }

    private void Buy(ShopItem item)
    {
        if (Wallet.TryBuyST(item.GetCost))
        {
            Wallet.BuyST(item.GetCost);
            PlayerAccessory.ChangeCurrentSkineIndex(item.GetIndexOnPlayer);
            SavePurcahse(item);
        }
        else
        {
            Debug.Log("You Don`t Have Money");
        }
    }

    private void SavePurcahse(ShopItem item)
    {
        switch(item.GetType)
        {
            case SubjectType.Accessory:
                Geekplay.Instance.PlayerData.BuyedAccessoryID.Add(item.GetIndexOnPlayer);
                break;
            case SubjectType.Item:
                if (!Geekplay.Instance.PlayerData.BuyedItemID.HasKey(item.GetIndexOnPlayer)) 
                    Geekplay.Instance.PlayerData.BuyedItemID.Add(new MyDictinary() { key = item.GetIndexOnPlayer });
                Geekplay.Instance.PlayerData.BuyedItemID.GetByKey(item.GetIndexOnPlayer).value++;
                break;
            case SubjectType.Light:
                Geekplay.Instance.PlayerData.BuyedLightID.Add(item.GetIndexOnPlayer);
                break;
            case SubjectType.Booster:
                if (!Geekplay.Instance.PlayerData.BuyedBoosterID.HasKey(item.GetIndexOnPlayer))
                    Geekplay.Instance.PlayerData.BuyedBoosterID.Add(new MyDictinary() { key = item.GetIndexOnPlayer });
                Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(item.GetIndexOnPlayer).value++;
                break;
        }

        Geekplay.Instance.Save();
    }

}
