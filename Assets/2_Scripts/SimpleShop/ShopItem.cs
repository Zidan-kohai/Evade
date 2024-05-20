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
    [SerializeField] private TextMeshProUGUI closeText;
    [SerializeField] private TextMeshProUGUI equipedTextView;
    [SerializeField] private TextMeshProUGUI buyedCountTextView;
    [SerializeField] private TextMeshProUGUI nameTextView;
    [SerializeField] private int buyedCount;
    [SerializeField] private bool isClose;

    public string GetName => data.name;
    public string GetDescription => data.description;
    public int GetCost => data.cost;
    public int GetIndexOnPlayer => data.indexOnPlayer;
    public int GetBuyedCount => buyedCount;
    public bool IsClose => isClose;
    public SubjectType GetType => data.type;

    public Sprite GetMainIcn => data.mainIcon;

    private void Start()
    {
        nameTextView.text = data.name;

        isClose = CompareLevel();

        if (IsClose) return;

        buyedCount = CheckBuyedCount();

        ChangeBuyedInfoText(buyedCount);

        CheckIsEquiped();

    }

    public void ChangeBuyedInfoText(int value)
    {
        if (value == 0 || data.type == SubjectType.Accessory || data.type == SubjectType.Light) return;

        buyedCountTextView.text = value.ToString();
        buyedCount = value;
    }

    public void Equip()
    {
        equipedTextView.gameObject.SetActive(true);
    }

    public void Dequip()
    {
        equipedTextView.gameObject.SetActive(false);
    }

    public void SubscribeEvent(Action action)
    {
        button.onClick.AddListener(() => action?.Invoke());
    }

    private void CheckIsEquiped()
    {
        switch(data.type)
        {
            case SubjectType.Accessory:
                if (data.oneTimePurchase && Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID == data.indexOnPlayer)
                {
                    Equip();
                }
            break;

            case SubjectType.Light:
                if (data.oneTimePurchase && Geekplay.Instance.PlayerData.CurrentEquipedLightID == data.indexOnPlayer)
                {
                    Equip();
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

    private bool CompareLevel()
    {
        if(data.openOnLevel >= Geekplay.Instance.PlayerData.Level)
        {
            closeText.text = $"откроется на {data.openOnLevel} уровне";
            closePanel.SetActive(true);
            return true;
        }
        return false;
    }
}
