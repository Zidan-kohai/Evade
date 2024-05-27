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

    public void Initialize(string description, int maxProgress, int currentProgress, int reward)
    {
        this.description.text = description;
        progress.text = $"{currentProgress}/{maxProgress}";

        this.reward.text = reward.ToString();
    }
}
