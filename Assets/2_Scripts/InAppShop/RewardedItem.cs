using GeekplaySchool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardedItem : MonoBehaviour
{
    [SerializeField] private GameObject closePanel;
    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private Button button;
    [SerializeField] private int goldCount;
    [SerializeField] private string rewardTag;

    private void Start()
    {
        Geekplay.Instance.SubscribeOnReward(rewardTag, Reward);

        button.onClick.AddListener(ShowADV);
    }

    private void Update()
    {
        if (!Geekplay.Instance.CanShowReward)
        {
            closePanel.SetActive(true);
            remainingText.text = string.Format("{0:f0}", Geekplay.Instance.RemainingTimeUntilRewardADV);
            button.enabled = false;
        }

        if (Geekplay.Instance.CanShowReward)
        {
            closePanel.SetActive(false);
            button.enabled = true;
        }
    }

    private void Reward()
    {
        Wallet.AddMoneyST(goldCount);
        Geekplay.Instance.RemainingTimeUntilRewardADV = Geekplay.Instance.TimeToShowReward;

        remainingText.text = Geekplay.Instance.RemainingTimeUntilRewardADV.ToString();

        Geekplay.Instance.Save();
    }

    private void ShowADV()
    {
        Geekplay.Instance.ShowRewardedAd(rewardTag);
    }
}
