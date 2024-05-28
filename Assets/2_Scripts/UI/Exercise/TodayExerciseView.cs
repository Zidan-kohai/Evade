using GeekplaySchool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodayExerciseView : MonoBehaviour
{
    [SerializeField] private List<DailyExerciseData> dates = new List<DailyExerciseData>();

    [SerializeField] private TodayExerciseItem itemPrefab;
    [SerializeField] private Transform itemParent;

    private void Start()
    {
        StartCoroutine(Wait(0.3f, ShowMainExercise));
    }

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }

    public void ShowMainExercise()
    {
        int todayIndex = Geekplay.Instance.PlayerData.EnterCount >=7 ? dates.Count -1 : Geekplay.Instance.PlayerData.EnterCount - 1;

        for (int j = 0; j < dates[todayIndex].ExerciseCount(); j++)
        {
            ExerciseProgress exercise = DailyExerciseController.Instance.GetExerciseInfo((Days)todayIndex, j);

            TodayExerciseItem exerciseView = Instantiate(itemPrefab, itemParent);

            exerciseView.Initialize(exercise.Description, exercise.Reward, exercise.GetProgress, exercise.GetMaxProgress);
        }
    }
}
