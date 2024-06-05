using GeekplaySchool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private TextMeshProUGUI rewardDescription;

    [SerializeField] private Slider progressSlider;
    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI claimButtonText;

    public void Initialize(Days day, int exerciseNumber, string description, int maxProgress, int currentProgress, int reward, bool isClaimed, AudioSource clickAudio)
    {
        if (Geekplay.Instance.language == "ru")
        {
            rewardDescription.text = "Награда:";
        }
        else if (Geekplay.Instance.language == "en")
        {
            rewardDescription.text = "Reward";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            rewardDescription.text = "Odul";
        }

        this.description.text = description;
        progress.text = $"{currentProgress}/{maxProgress}";

        progressSlider.minValue = 0;
        progressSlider.maxValue = maxProgress;
        progressSlider.value = currentProgress;

        this.reward.text = reward.ToString();

        if(currentProgress == maxProgress && !isClaimed && Geekplay.Instance.PlayerData.EnterCount > (int)day)
        {
            claimButton.gameObject.SetActive(true);

            if(Geekplay.Instance.language == "ru")
            {
                claimButtonText.text = "Забрать";
            }
            else if (Geekplay.Instance.language == "en")
            {
                claimButtonText.text = "Claim";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                claimButtonText.text = "Iddia";
            }

            claimButton.onClick.AddListener(() =>
            {
                clickAudio.Play();
                Wallet.AddMoneyST(reward);
                claimButton.gameObject.SetActive(false);
                Geekplay.Instance.PlayerData.SetExerciseClaim(day, exerciseNumber);
            });
        }
    }
}
