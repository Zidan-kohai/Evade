using TMPro;
using UnityEngine;

public class TodayExerciseItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionView;
    [SerializeField] private TextMeshProUGUI rewardView;
    [SerializeField] private TextMeshProUGUI percentView;


    public void Initialize(string description, int reward, float currentProgress, float maxProgress)
    {
        descriptionView.text = description;
        rewardView.text = reward.ToString();

        percentView.text = $"{((currentProgress / maxProgress) * 100)}%";
    }
}
