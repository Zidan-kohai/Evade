using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyExerciseView : MonoBehaviour
{
    [SerializeField] private Button day1;
    [SerializeField] private Button day2;
    [SerializeField] private Button day3;
    [SerializeField] private Button day4;
    [SerializeField] private Button day5;
    [SerializeField] private Button day6;
    [SerializeField] private Button day7;

    [SerializeField] private ExerciseView exerciseViewPrefab;
    [SerializeField] private Transform exerciseViewParent;

    [SerializeField] private List<DailyExerciseData> dates = new List<DailyExerciseData>();

    private List<ExerciseView> exerciseViewHandler = new List<ExerciseView>();

    private void OnEnable()
    {
        SpawnDaysExercise((int)Days.Day1);
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

}
