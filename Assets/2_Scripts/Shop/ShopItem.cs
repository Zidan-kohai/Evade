using GeekplaySchool;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopItemData data;
    [SerializeField] private Button button;

    [SerializeField] private GameObject closePanel;
    [SerializeField] private TextMeshProUGUI EquipedTextView;
    [SerializeField] private TextMeshProUGUI nameTextView;

    public string GetDataName => data.name;
    public string GetDescription => data.description;
    public string GetDataCost => data.cost;


    private void Awake()
    {
        nameTextView.text = data.name;

        if (data.oneTimePurchase && CheckIsBuy())
        {
            EquipedTextView.gameObject.SetActive(false);
        }
    }

    public bool CheckIsBuy()
    {

        switch(data.type)
        {
            case SubjectType.Accessory:
                foreach (var item in Geekplay.Instance.PlayerData.BuyedAccessoryID)
                {
                    if(item.key == data.indexOnPlayer)
                    {
                        return true;
                    }
                }
                break;
            case SubjectType.Item:
                foreach (var item in Geekplay.Instance.PlayerData.BuyedItemID)
                {
                    if (item.key == data.indexOnPlayer)
                    {
                        return true;
                    }
                }
                break;
            case SubjectType.Light:
                foreach (var item in Geekplay.Instance.PlayerData.BuyedLightID)
                {
                    if (item.key == data.indexOnPlayer)
                    {
                        return true;
                    }
                }
                break;
            case SubjectType.Booster:
                foreach (var item in Geekplay.Instance.PlayerData.BuyedBoosterID)
                {
                    if (item.key == data.indexOnPlayer)
                    {
                        return true;
                    }
                }
                break;
        }

        return false;
    }

    public void SubscribeEvent(Action action)
    {
        button.onClick.AddListener(() => action?.Invoke());
    }

}
