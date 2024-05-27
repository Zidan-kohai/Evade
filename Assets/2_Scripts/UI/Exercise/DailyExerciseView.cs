using GeekplaySchool;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DailyExerciseView : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private List<ExerciseDayButton> daysButton;

    [SerializeField] private ExerciseView exerciseViewPrefab;
    [SerializeField] private Transform exerciseViewParent;

    [SerializeField] private List<DailyExerciseData> dates = new List<DailyExerciseData>();

    private List<ExerciseView> exerciseViewHandler = new List<ExerciseView>();

    private void OnEnable()
    {
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
            ExerciseProgress exercise = DailyExerciseController.Instance.GetInfo((Days)day, j);

            ExerciseView exerciseView = Instantiate(exerciseViewPrefab, exerciseViewParent);
            exerciseView.Initialize(exercise.Description, exercise.GetMaxProgress, exercise.GetProgress, exercise.Reward);

            exerciseViewHandler.Add(exerciseView);
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
