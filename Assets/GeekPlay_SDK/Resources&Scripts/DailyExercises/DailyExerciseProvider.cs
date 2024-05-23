using System;
using UnityEngine;

[Serializable]
public class DailyExerciseProvider
{
    [SerializeField] private DailyExercise dailyExercise;
    [SerializeField] private DailyExerciseData dailyExerciseData;

    public void SetProggres(int exerciseNumber)
    {
        dailyExercise.SetPrograss(exerciseNumber);
    }
}
