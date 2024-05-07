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
    [SerializeField] private int buyedCount;
    public string GetDataName => data.name;
    public string GetDescription => data.description;
    public string GetDataCost => data.cost;
    public int GetBuyedCount => buyedCount;

    private void Start()
    {

        nameTextView.text = data.name;

        buyedCount = CheckBuyedCount();

        CheckIsEquiped();
        
    }

    private void CheckIsEquiped()
    {
        switch(data.type)
        {
            case SubjectType.Accessory:
                if (data.oneTimePurchase && Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID == data.indexOnPlayer)
                {
                    EquipedTextView.gameObject.SetActive(true);
                }
            break;

            case SubjectType.Light:
                if (data.oneTimePurchase && Geekplay.Instance.PlayerData.CurrentEquipedLightID == data.indexOnPlayer)
                {
                    EquipedTextView.gameObject.SetActive(true);
                }
            break;
        }
    }

    private int CheckBuyedCount()
    {
        int count = 0;
        switch(data.type)
        {
            case SubjectType.Accessory:

                if(Geekplay.Instance.PlayerData.BuyedAccessoryID.Contains(data.indexOnPlayer))
                {
                    count = 1;
                }
                break;
            case SubjectType.Item:
                foreach (var item in Geekplay.Instance.PlayerData.BuyedItemID)
                {
                    if (item.key == data.indexOnPlayer)
                    {
                        count = item.value;
                    }
                }
                break;
            case SubjectType.Light:
                if(Geekplay.Instance.PlayerData.BuyedLightID.Contains(data.indexOnPlayer))
                {
                    count = 1;
                }
                break;
            case SubjectType.Booster:
                foreach (var item in Geekplay.Instance.PlayerData.BuyedBoosterID)
                {
                    if (item.key == data.indexOnPlayer)
                    {
                        count = item.value;
                    }
                }
                break;
        }

        return count;
    }

    public void SubscribeEvent(Action action)
    {
        button.onClick.AddListener(() => action?.Invoke());
    }

}
