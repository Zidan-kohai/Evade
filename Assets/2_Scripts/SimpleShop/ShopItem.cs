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
    [SerializeField] private Image Icon;
    [SerializeField] private int buyedCount;
    [SerializeField] private bool isClose;

    public string GetName => data.Name();
    public string GetDescription => data.Description();
    public int GetCost => data.cost;
    public int GetIndexOnPlayer => data.indexOnPlayer;
    public int GetBuyedCount => buyedCount;
    public bool IsClose => isClose;
    public SubjectType GetType => data.type;

    public Sprite GetMainIcn => data.mainIcon;

    private void OnEnable()
    {
        nameTextView.text = data.Name();

        buyedCount = CheckBuyedCount();

        ChangeBuyedInfoText(buyedCount);

        CheckIsEquiped();

        if(buyedCount == 0)
            isClose = CompareLevel();
    }

    public void ChangeBuyedInfoText(int value)
    {
        Icon.sprite = data.mainIcon;
        Icon.rectTransform.sizeDelta = data.mainIconSize;

        if (value == 0 || data.type == SubjectType.Accessory || data.type == SubjectType.Light) return;

        buyedCountTextView.text = value.ToString();
        buyedCount = value;
    }

    public void Equip()
    {
        if (Geekplay.Instance.language == "ru")
        {
            equipedTextView.text = "Экипировано";
        }
        else if (Geekplay.Instance.language == "en")
        {
            equipedTextView.text = "Equipped";
        }
        else
        {
            equipedTextView.text = "Equipped";
        }

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
            case SubjectType.Item:
                if (data.oneTimePurchase && Geekplay.Instance.PlayerData.CurrentEquipedItemID == data.indexOnPlayer)
                {
                    Equip();
                }
                break;
            case SubjectType.Booster:
                if (Geekplay.Instance.PlayerData.CurrentBoosterKeys.Contains(data.indexOnPlayer))
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
                if (Geekplay.Instance.PlayerData.BuyedItemID.Contains(data.indexOnPlayer))
                {
                    count = 1;
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
            if(Geekplay.Instance.language == "ru")
            {
                closeText.text = $"Откроется на {data.openOnLevel} уровне";
            }
            else if(Geekplay.Instance.language == "en")
            {
                closeText.text = $"Open on {data.openOnLevel} level";
            }
            else
            {
                closeText.text = $"{data.openOnLevel} seviyede ac";
            }

            closePanel.SetActive(true);
            return true;
        }
        return false;
    }
}
