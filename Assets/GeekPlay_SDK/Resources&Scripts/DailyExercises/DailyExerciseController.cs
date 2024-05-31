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

        DontDestroyOnLoad(Instance);

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

    public void SetProgress(Days day, int exerciseNumber, int progressStep = 1)
    {
        DailyExercise dailyExercise = dailyExercises.GetValueByKey(day);
        int progress = dailyExercise.SetProgress(exerciseNumber, progressStep);

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

    public void GetPercentOfDoneExercise(Days day, out int currentProgress, out int maxProgress)
    {
        maxProgress = 0;
        currentProgress = 0;

        foreach (var item in dailyExercises)
        {
            if (item.day == day)
            {
                maxProgress = item.dailyExercise.ExerciseCount;

                for (int i = 0; i < maxProgress; i++)
                {
                    if (item.dailyExercise.GetExercise(i).IsDone)
                    {
                        currentProgress++;
                    }
                }
            }
        }

    }

    public bool HasExercise(Days day, int exerciseNumber)
    {
        foreach (var item in dailyExercises)
        {
            if (item.day == day)
            {
                return item.dailyExercise.HasExercise(exerciseNumber);
            }
        }

        return false;
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