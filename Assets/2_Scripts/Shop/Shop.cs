using GeekplaySchool;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;

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
        ShowItemInfo(firstItem);

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
        ShowItemInfo(item);
        buyButton.onClick.RemoveAllListeners();
        equipButton.onClick.RemoveAllListeners();

        switch(item.GetType)
        {
            case SubjectType.Accessory:
                EventForAccessory(item);
                break;
            case SubjectType.Item:
                EventForItem(item);
                break;
            case SubjectType.Light:
                EventForLight(item);
                break;
            case SubjectType.Booster:
                EventForBooster(item);
                break;
        }

    }

    private void EventForBooster(ShopItem item)
    {
        buyButton.gameObject.SetActive(true);
        equipButton.gameObject.SetActive(false);

        buyButton.onClick.AddListener(() =>
        {
            Buy(item);
        });
    }

    private void EventForLight(ShopItem item)
    {
        if (!Geekplay.Instance.PlayerData.BuyedLightID.Contains(item.GetIndexOnPlayer))
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);

            buyButton.onClick.AddListener(() =>
            {
                Buy(item);
            });
        }
        else
        {
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            equipButton.onClick.AddListener(() =>
            {
                Equip(item);
            });
        }
    }

    private void EventForItem(ShopItem item)
    {
        buyButton.gameObject.SetActive(true);
        equipButton.gameObject.SetActive(false);

        buyButton.onClick.AddListener(() =>
        {
            Buy(item);
        });
    }

    private void EventForAccessory(ShopItem item)
    {
        if (!Geekplay.Instance.PlayerData.BuyedAccessoryID.Contains(item.GetIndexOnPlayer))
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false); 

            buyButton.onClick.AddListener(() =>
            {
                Buy(item);
            });
        }
        else
        {
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            equipButton.onClick.AddListener(() =>
            {
                Equip(item);
            });
        }
    }

    private void Equip(ShopItem item)
    {
        switch(item.GetType)
        {
            case SubjectType.Accessory:
                EquipAccessory(item);
                break;
            case SubjectType.Item:
                break;
            case SubjectType.Light:
                EquipLight(item);
                break;
            case SubjectType.Booster:
                break;

        }
    }

    private void EquipAccessory(ShopItem item)
    {
        PlayerAccessory.ChangeCurrentSkineIndex(item.GetIndexOnPlayer);

        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].GetType == SubjectType.Accessory)
            {
                items[i].Dequip();
            }
        }

        item.Equip();
    }

    private void EquipLight(ShopItem item)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetType == SubjectType.Light)
            {
                items[i].Dequip();
            }
        }

        item.Equip();
    }

    private void ShowItemInfo(ShopItem item)
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
            Equip(item);
            SavePurcahse(item);

            Event(item);
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
