using GeekplaySchool;
using System;
using UnityEngine;
using Exercise;
using System.Collections.Generic;

[Serializable]
public class DailyExerciseController : MonoBehaviour
{
    public static DailyExerciseController Instance { get; private set; }

    [SerializeField] private List<ExerciseDictionary> dailyExercises = new List<ExerciseDictionary>();

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;

        foreach (var item in dailyExercises)
        {
            item.dailyExercise.Init();
        }

        foreach(DayExerciseSaveData item in Geekplay.Instance.PlayerData.exercises)
        {
            foreach (ExerciseDictionary dailyExercise in dailyExercises)
            {
                if(item.day == dailyExercise.day)
                {
                    for (int i = 0; i < item.exerciseSaveData.Count; i++)
                    {
                        dailyExercise.dailyExercise.SetProgress(i, item.exerciseSaveData[i].exerciseProgress);
                    }
                }
            }
        }

    }

    public void SetProgress(Days day, int exerciseNumber)
    {
        DailyExercise dailyExercise = dailyExercises.GetValueByKey(day);
        int progress = dailyExercise.SetProgress(exerciseNumber);

        Geekplay.Instance.PlayerData.SetExerciseProgress(day, exerciseNumber, progress);
    }

    public ExerciseProgress GetInfo(Days day, int exerciseNumber)
    {
        foreach (var item in dailyExercises)
        {
            if(item.day == day)
            {
                return item.dailyExercise.GetExercise(exerciseNumber);
            }
        }

        return null;
    }
}