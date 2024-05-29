using GeekplaySchool;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TodayExerciseView : MonoBehaviour
{
    [SerializeField] private List<DailyExerciseData> dates = new List<DailyExerciseData>();

    [SerializeField] private TodayExerciseItem itemPrefab;
    [SerializeField] private Transform itemParent;
    [SerializeField] private TextMeshProUGUI headerText;

    [SerializeField] private List<TodayExerciseItem> todayExerciseItems = new List<TodayExerciseItem>();

    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            headerText.text = "≈жедневные награды";
        }
        else if (Geekplay.Instance.language == "en")
        {
            headerText.text = "Dayli Exercise";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            headerText.text = "Gunluk Oduller";
        }
    }

    private void OnEnable()
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
        Destroy();

        int todayIndex = Geekplay.Instance.PlayerData.EnterCount >=7 ? dates.Count -1 : Geekplay.Instance.PlayerData.EnterCount - 1;

        for (int j = 0; j < dates[todayIndex].ExerciseCount(); j++)
        {
            ExerciseProgress exercise = DailyExerciseController.Instance.GetExerciseInfo((Days)todayIndex, j);

            TodayExerciseItem exerciseView = Instantiate(itemPrefab, itemParent);

            exerciseView.Initialize(exercise.Description, exercise.Reward, exercise.GetProgress, exercise.GetMaxProgress);

            todayExerciseItems.Add(exerciseView);
        }

        StartCoroutine(Wait(5f, ShowMainExercise));
    }


    private void Destroy()
    {
        while(todayExerciseItems.Count > 0)
        {
            GameObject gObject = todayExerciseItems[0].gameObject;
            todayExerciseItems.RemoveAt(0);
            Destroy(gObject);
        }
    }
}
