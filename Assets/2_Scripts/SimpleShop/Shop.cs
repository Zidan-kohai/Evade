using GeekplaySchool;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private TextMeshProUGUI equipTextView;
    [SerializeField] private RectTransform closeButton;

    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI descriptionView;
    [SerializeField] private TextMeshProUGUI costView;

    [SerializeField] private List<ShopItem> items;
    [SerializeField] private ShopItem firstItem;

    [SerializeField] private BoosterSwitcher boosterSwitcher;

    private void OnEnable()
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

        if(item.IsClose)
        {
            buyButton.gameObject.SetActive(false); 
            equipButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            return;
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }

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
        

        if (item.GetBuyedCount > 0)
        {
            equipButton.gameObject.SetActive(true);

            if (Geekplay.Instance.PlayerData.CurrentBoosterKeys.Contains(item.GetIndexOnPlayer))
            {
                ChangeTextEquipped();
            }
            else
            {
                ChangeTextEquip();

                equipButton.onClick.AddListener(() =>
                {
                    boosterSwitcher.Initialize(item, equipTextView);
                });
            }
        }

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
                if(item.GetIndexOnPlayer == 2)
                {
                    DailyExerciseController.Instance.SetProgress(Days.Day3, 4);
                }
                if (item.GetIndexOnPlayer == 1)
                {
                    DailyExerciseController.Instance.SetProgress(Days.Day1, 4);
                }
                Buy(item);
            });
        }
        else if(Geekplay.Instance.PlayerData.CurrentEquipedLightID != item.GetIndexOnPlayer)
        {
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            ChangeTextEquip();

            equipButton.onClick.AddListener(() =>
            {
                Equip(item);
                ChangeTextEquipped();
            });
        }
        else
        {
            ChangeTextEquipped();
        }
    }

    private void EventForItem(ShopItem item)
    {
        if (!Geekplay.Instance.PlayerData.BuyedItemID.Contains(item.GetIndexOnPlayer))
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);

            buyButton.onClick.AddListener(() =>
            {
                if (item.GetIndexOnPlayer == 1)
                {
                    DailyExerciseController.Instance.SetProgress(Days.Day2, 2);
                }
                Buy(item);
            });
        }
        else if(Geekplay.Instance.PlayerData.CurrentEquipedItemID != item.GetIndexOnPlayer)
        {
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            ChangeTextEquip();

            equipButton.onClick.AddListener(() =>
            {
                Equip(item);

                ChangeTextEquipped();
            });
        }
        else
        {
            ChangeTextEquipped();
        }
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
        else if(Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID != item.GetIndexOnPlayer)
        {
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            ChangeTextEquip();

            equipButton.onClick.AddListener(() =>
            {
                DailyExerciseController.Instance.SetProgress(Days.Day3, 4);

                Equip(item);
                ChangeTextEquipped();
            });
        }
        else
        {
            ChangeTextEquipped();
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
                EquipItem(item);
                break;
            case SubjectType.Light:
                EquipLight(item);
                break;
            case SubjectType.Booster:
                EquipBooster(item);
                break;

        }
    }

    private void EquipBooster(ShopItem item)
    {
        for(int i = 0; i < 3; i++)
        {
            if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == -1 && !Geekplay.Instance.PlayerData.CurrentBoosterKeys.Contains(item.GetIndexOnPlayer))
            {
                Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] = item.GetIndexOnPlayer;
                item.Equip();
                break;
            }
        }

        Geekplay.Instance.Save();
    }

    private void EquipItem(ShopItem item)
    {
        Geekplay.Instance.PlayerData.CurrentEquipedItemID = item.GetIndexOnPlayer;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetType == SubjectType.Item)
            {
                items[i].Dequip();
            }
        }

        item.Equip();
        Geekplay.Instance.Save();
    }

    private void EquipAccessory(ShopItem item)
    {
        Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID = item.GetIndexOnPlayer;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetType == SubjectType.Accessory)
            {
                items[i].Dequip();
            }
        }

        item.Equip();
        Geekplay.Instance.Save();
    }

    private void EquipLight(ShopItem item)
    {
        Geekplay.Instance.PlayerData.CurrentEquipedLightID = item.GetIndexOnPlayer;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetType == SubjectType.Light)
            {
                items[i].Dequip();
            }
        }

        item.Equip();
        Geekplay.Instance.Save();
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
                Geekplay.Instance.PlayerData.BuyedItemID.Add(item.GetIndexOnPlayer);
                break;
            case SubjectType.Light:
                Geekplay.Instance.PlayerData.BuyedLightID.Add(item.GetIndexOnPlayer);
                break;
            case SubjectType.Booster:
                if (!Geekplay.Instance.PlayerData.BuyedBoosterID.HasKey(item.GetIndexOnPlayer))
                    Geekplay.Instance.PlayerData.BuyedBoosterID.Add(new MyDictionary() { key = item.GetIndexOnPlayer });

                Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(item.GetIndexOnPlayer).value++;
                item.ChangeBuyedInfoText(Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(item.GetIndexOnPlayer).value);
                break;
        }


        Geekplay.Instance.Save();
    }


    private void ChangeTextEquipped()
    {
        if(Geekplay.Instance.language == "ru")
        {
            equipTextView.text = "Ёкипирован";
        }
        else if (Geekplay.Instance.language == "en")
        {
            equipTextView.text = "Equipped";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            equipTextView.text = "Donanimli";
        }
    }

    private void ChangeTextEquip()
    {
        if (Geekplay.Instance.language == "ru")
        {
            equipTextView.text = "Ёкипировать";
        }
        else if (Geekplay.Instance.language == "en")
        {
            equipTextView.text = "Equip";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            equipTextView.text = "Donatmak";
        }
    }
}
