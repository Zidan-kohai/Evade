using GeekplaySchool;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InAppShopItem : MonoBehaviour
{
    [SerializeField] private InAppShopItemData data;
    [SerializeField] private TextMeshProUGUI goldRewardView;
    [SerializeField] private TextMeshProUGUI diamondRewardView;
    [SerializeField] private TextMeshProUGUI costView;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(PurchaseEvent);

        ShowInfo();
    }

    public void SubscribeReward()
    {
        Geekplay.Instance.SubscribeOnPurshace(data.PurchaseTag, Reward);
    }

    private void ShowInfo()
    {
        switch(data.RewardType)
        {
            case RewardType.Gold:
                goldRewardView.text = $"{data.Goldcount}$";
                break;
            case RewardType.Diamond:
                diamondRewardView.text = $"{data.DiamondCount}$";
                break;
            case RewardType.Both:
                goldRewardView.text = $"{data.Goldcount}$";
                diamondRewardView.text = $"{data.DiamondCount}$";
                break;
        }

        if(Geekplay.Instance.language == "ru")
        {
            costView.text = $"{data.Cost} ян";
        }
        else if(Geekplay.Instance.language == "en" || Geekplay.Instance.language == "tr")
        {
            costView.text = $"{data.Cost} Yan";
        }
    }

    private void Reward()
    {
        Geekplay.Instance.StartCoroutine(Wait(() =>
        {
            Debug.Log("Start Reward");
            Wallet.AddMoneyST(data.Goldcount);

            Geekplay.Instance.PlayerData.DonatCount += data.Cost;
            Geekplay.Instance.SetLeaderboard(Helper.DonatLeaderboardName, Geekplay.Instance.PlayerData.DonatCount);
            Geekplay.Instance.Save();
            Debug.Log("End Reward");
        }));
        
    }

    private IEnumerator Wait(Action action)
    {
        yield return new WaitForSeconds(0.5f);

        action?.Invoke();
    }

    private void PurchaseEvent()
    {
        Geekplay.Instance.RealBuyItem(data.PurchaseTag);
    }
}
