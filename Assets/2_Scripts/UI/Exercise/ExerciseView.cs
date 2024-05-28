using GeekplaySchool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI reward;

    [SerializeField] private Slider progressSlider;
    [SerializeField] private Button claimButton;

    public void Initialize(Days day, int exerciseNumber, string description, int maxProgress, int currentProgress, int reward, bool isClaimed)
    {
        this.description.text = description;
        progress.text = $"{currentProgress}/{maxProgress}";

        progressSlider.minValue = 0;
        progressSlider.maxValue = maxProgress;
        progressSlider.value = currentProgress;

        this.reward.text = reward.ToString();

        if(currentProgress == maxProgress && !isClaimed && Geekplay.Instance.PlayerData.EnterCount > (int)day)
        {
            claimButton.gameObject.SetActive(true);

            claimButton.onClick.AddListener(() =>
            {
                Wallet.AddMoneyST(reward);
                claimButton.gameObject.SetActive(false);
                Geekplay.Instance.PlayerData.SetExerciseClaim(day, exerciseNumber);
            });
        }
    }
}
