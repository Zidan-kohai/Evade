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

    [SerializeField] private TextMeshProUGUI dailyHeaderText;
    [SerializeField] private TextMeshProUGUI dailyRewardText;
    [SerializeField] private Button dailyClaimButton;
    [SerializeField] private TextMeshProUGUI dailyClaimButtonText;
    [SerializeField] private Slider dailyClaimSlider;
    [SerializeField] private TextMeshProUGUI dailySliderValue;

    [SerializeField] private ExerciseView exerciseViewPrefab;
    [SerializeField] private Transform exerciseViewParent;

    [SerializeField] private List<DailyExerciseData> dates = new List<DailyExerciseData>();

    private List<ExerciseView> exerciseViewHandler = new List<ExerciseView>();

    private void Start()
    {
    }

    private void OnEnable()
    {
        ActivateButton();

        SpawnDaysExercise((int)Days.Day1);

    }

    public bool HasRewardThatNoneClaim()
    {
        for (int day = 0; day < dates.Count; day++)
        {
            for (int j = 0; j < dates[day].ExerciseCount(); j++)
            {
                if (!DailyExerciseController.Instance.HasExercise((Days)day, j) || day >= Geekplay.Instance.PlayerData.EnterCount) continue;

                ExerciseProgress exercise = DailyExerciseController.Instance.GetExerciseInfo((Days)day, j);

                bool isClaimed = Geekplay.Instance.PlayerData.IsExerciseClaim((Days)day, j);

                if(!isClaimed && exercise.GetMaxProgress <= exercise.GetProgress)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void ActivateButton()
    {
        for(int i = 0; i < dates.Count; i++)
        {
            int dayIndex = i;

            if(dayIndex < Geekplay.Instance.PlayerData.EnterCount)
                daysButton[i].closePanel.gameObject.SetActive(false);

            daysButton[i].button.onClick.AddListener(() =>
            {
                DestroyCurrnentExercices();
                SpawnDaysExercise(dayIndex);
            });
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

        if (Geekplay.Instance.language == "ru")
        {
            dailyHeaderText.text = $"Специальный приз \n день {day + 1}";
        }
        else if (Geekplay.Instance.language == "en")
        {
            dailyHeaderText.text = $"Special Prize \n day {day + 1}";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            dailyHeaderText.text = $"Ozel odul \n gun {day + 1}";
        }

        dailyRewardText.text = dates[day].RewardMoney.ToString();


        DailyExerciseController.Instance.GetPercentOfDoneExercise((Days)day, out int currentProgress, out int maxProgress);

        dailyClaimSlider.minValue = 0;
        dailyClaimSlider.maxValue = maxProgress;
        dailyClaimSlider.value = currentProgress;
        dailySliderValue.text = $"{currentProgress}/{maxProgress}";

        if (DailyExerciseController.Instance.GetIsDayDone((Days)day))
        {
            dailyClaimButton.gameObject.SetActive(true);

            if (Geekplay.Instance.PlayerData.IsDayExerciseRewardClaim((Days)day))
            {
                
                if (Geekplay.Instance.language == "ru")
                {
                    dailyClaimButtonText.text = "Забрали";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    dailyClaimButtonText.text = "Claimed";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    dailyClaimButtonText.text = "Aldi";
                }
            }
            else
            {
                if (Geekplay.Instance.language == "ru")
                {
                    dailyClaimButtonText.text = "Забрать";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    dailyClaimButtonText.text = "Claim";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    dailyClaimButtonText.text = "Iddia";
                }

                dailyClaimButton.onClick.AddListener(() =>
                {
                    Wallet.AddMoneyST(dates[day].RewardMoney);
                    Geekplay.Instance.PlayerData.SetDayExerciseRewardClaim((Days)day);
                    dailyClaimButton.onClick.RemoveAllListeners();
                    if (Geekplay.Instance.language == "ru")
                    {
                        dailyClaimButtonText.text = "Забрали";
                    }
                    else if (Geekplay.Instance.language == "en")
                    {
                        dailyClaimButtonText.text = "Claimed";
                    }
                    else if (Geekplay.Instance.language == "tr")
                    {
                        dailyClaimButtonText.text = "Aldi";
                    }
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
