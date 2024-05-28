using GeekplaySchool;
using System;
using UnityEngine;
using Exercise;
using System.Collections.Generic;
using static UnityEditor.Progress;

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

        //Fill exercise with default Data
        foreach (var item in dailyExercises)
        {
            item.dailyExercise.Init();
        }

        //Set exercise Data with saved Data
        foreach(DayExerciseSaveData item in Geekplay.Instance.PlayerData.exercises)
        {
            foreach (ExerciseDictionary dailyExercise in dailyExercises)
            {
                if(item.day == dailyExercise.day)
                {
                    for (int i = 0; i < item.exerciseSaveData.Count; i++)
                    {
                        dailyExercise.dailyExercise.SetProgress(i, item.exerciseSaveData[i].ExerciseProgress);
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

    public bool GetIsDayDone(Days day)
    {
        foreach (ExerciseDictionary dailyExercise in dailyExercises)
        {
            if (dailyExercise.day == day)
            {
                return dailyExercise.dailyExercise.IsDone();
            }
        }

        return false;
    }

    public void GetPercentOfDoneExercise(out int currentProgress, out int maxProgress)
    {
        maxProgress = dailyExercises.Count;

        currentProgress = 0;

        for (int i = 0; i < maxProgress; i++)
        {
            if(dailyExercises[i].dailyExercise.IsDone())
            {
                currentProgress++;
            }
        }
    }

    public ExerciseProgress GetExerciseInfo(Days day, int exerciseNumber)
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