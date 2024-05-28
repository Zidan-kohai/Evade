using GeekplaySchool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InAppShopItem : MonoBehaviour
{
    [SerializeField] private InAppShopItemData data;
    [SerializeField] private TextMeshProUGUI goldRewardView;
    [SerializeField] private TextMeshProUGUI diamondRewardView;
    [SerializeField] private TextMeshProUGUI costView;
    [SerializeField] private GameObject closePanel;
    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private Button button;

    private void Awake()
    {
        switch (data.BuyType)
        {
            case BuyType.InApp:
                button.onClick.AddListener(PurchaseEvent);
                break;
            case BuyType.Video:
                button.onClick.AddListener(ShowADV);
                break;
            default:
                break;
        }

        ShowInfo();
    }

    public void Update()
    {
        if(data.BuyType == BuyType.Video && !Geekplay.Instance.CanShowReward)
        {
            closePanel.SetActive(true);
            remainingText.text = string.Format("{0:f0}", Geekplay.Instance.RemainingTimeUntilRewardADV);
            button.enabled = false;
        }

        if(data.BuyType == BuyType.Video && Geekplay.Instance.CanShowReward)
        {
            closePanel.SetActive(false);
            button.enabled = true;
        }
    }

    public void SubscribeReward()
    {
        switch (data.BuyType)
        {
            case BuyType.InApp:
                Geekplay.Instance.SubscribeOnPurshace(data.PurchaseTag, Reward);
                break;
            case BuyType.Video:
                Geekplay.Instance.SubscribeOnReward(data.PurchaseTag, Reward);
                break;
            default:
                break;
        }

    }

    private void ShowInfo()
    {
        switch(data.RewardType)
        {
            case RewardType.Gold:
                goldRewardView.text = data.Goldcount.ToString();
                break;
            case RewardType.Diamond:
                diamondRewardView.text = data.DiamondCount.ToString();
                break;
            case RewardType.Both:
                goldRewardView.text = data.Goldcount.ToString();
                diamondRewardView.text = data.DiamondCount.ToString();
                break;
        }
        if(data.BuyType == BuyType.InApp)
            costView.text = data.Cost.ToString();
    }

    private void Reward()
    {
        Wallet.AddMoneyST(data.Goldcount);

        closePanel.SetActive(true);
        Geekplay.Instance.RemainingTimeUntilRewardADV = Geekplay.Instance.TimeToShowReward;

        remainingText.text = Geekplay.Instance.RemainingTimeUntilRewardADV.ToString();

        Geekplay.Instance.PlayerData.DonatCount += data.Cost;
        Geekplay.Instance.SetLeaderboard(Helper.DonatLeaderboardName, Geekplay.Instance.PlayerData.DonatCount);
        Geekplay.Instance.Save();
    }

    private void ShowADV()
    {
        Geekplay.Instance.ShowRewardedAd(data.PurchaseTag);
    }

    private void PurchaseEvent()
    {
        Geekplay.Instance.RealBuyItem(data.PurchaseTag);
    }
}
