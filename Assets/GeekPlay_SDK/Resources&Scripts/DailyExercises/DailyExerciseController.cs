using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyExerciseController : MonoBehaviour
{
    [SerializeField] private List<DailyExerciseProvider> dailyExerciseProviders = new List<DailyExerciseProvider>();


    public void SetProgress(int day, int exerciseNumber)
    {
        dailyExerciseProviders[day].SetProggres(exerciseNumber);
    }
}