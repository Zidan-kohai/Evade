using GeekplaySchool;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyExerciseView : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private List<ExerciseDayButton> daysButton;

    [SerializeField] private TextMeshProUGUI dailyRewardText;
    [SerializeField] private Button dailyClaimButton;
    [SerializeField] private TextMeshProUGUI dailyClaimButtonText;
    [SerializeField] private Slider dailyClaimSlider;

    [SerializeField] private ExerciseView exerciseViewPrefab;
    [SerializeField] private Transform exerciseViewParent;

    [SerializeField] private List<DailyExerciseData> dates = new List<DailyExerciseData>();

    private List<ExerciseView> exerciseViewHandler = new List<ExerciseView>();

    private void OnEnable()
    {
        DailyExerciseController.Instance.SetProgress(Days.Day1, 1);
        DailyExerciseController.Instance.SetProgress(Days.Day1, 2);
        DailyExerciseController.Instance.SetProgress(Days.Day1, 3);
        DailyExerciseController.Instance.SetProgress(Days.Day1, 0);

        ActivateButton();

        SpawnDaysExercise((int)Days.Day1);

    }

    private void ActivateButton()
    {
        for(int i = 0; i < Geekplay.Instance.PlayerData.EnterCount; i++)
        {
            if(i < dates.Count)
            {
                int dayIndex = i;
                daysButton[i].closePanel.gameObject.SetActive(false);
                daysButton[i].button.onClick.AddListener(() => 
                {
                    DestroyCurrnentExercices();
                    SpawnDaysExercise(dayIndex);
                });
            }
        }
    }

    public void SpawnDaysExercise(int day)
    {
        for (int j = 0; j < dates[day].ExerciseCount(); j++)
        {
            ExerciseProgress exercise = DailyExerciseController.Instance.GetExerciseInfo((Days)day, j);

            ExerciseView exerciseView = Instantiate(exerciseViewPrefab, exerciseViewParent);

            bool isClaimed = Geekplay.Instance.PlayerData.IsExerciseClaim((Days)day, j);

            exerciseView.Initialize((Days)day, j, exercise.Description, exercise.GetMaxProgress, exercise.GetProgress, exercise.Reward, isClaimed);

            exerciseViewHandler.Add(exerciseView);
        }

        dailyRewardText.text = dates[day].RewardMoney.ToString();


        DailyExerciseController.Instance.GetPercentOfDoneExercise((Days)day, out int currentProgress, out int maxProgress);

        dailyClaimSlider.minValue = 0;
        dailyClaimSlider.maxValue = maxProgress;
        dailyClaimSlider.value = currentProgress;

        if (DailyExerciseController.Instance.GetIsDayDone((Days)day))
        {
            dailyClaimButton.gameObject.SetActive(true);

            if (Geekplay.Instance.PlayerData.IsDayExerciseRewardClaim((Days)day))
            {
                dailyClaimButtonText.text = "Забрали";
            }
            else
            {
                dailyClaimButtonText.text = "Забрать";

                dailyClaimButton.onClick.AddListener(() =>
                {
                    Wallet.AddMoneyST(dates[day].RewardMoney);
                    Geekplay.Instance.PlayerData.SetDayExerciseRewardClaim((Days)day);
                    dailyClaimButton.onClick.RemoveAllListeners();
                    dailyClaimButtonText.text = "Забрали";
                });
            }
        }
        else
        {
            dailyClaimButton.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        DestroyCurrnentExercices();
    }

    private void DestroyCurrnentExercices()
    {
        while (exerciseViewHandler.Count > 0)
        {
            GameObject gobject = exerciseViewHandler[0].gameObject;
            exerciseViewHandler.RemoveAt(0);
            Destroy(gobject);
        }
    }

    [Serializable]
    public class ExerciseDayButton
    {
        public Button button;
        public GameObject closePanel; 
    }
}
