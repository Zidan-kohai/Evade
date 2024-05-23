using System;
using System.Collections.Generic;

[Serializable]
public class DailyExerciseController
{
    List<DailyExerciseProvider> dailyExerciseProviders = new List<DailyExerciseProvider>();


    public void SetProgress(int day, int exerciseNumber)
    {
        dailyExerciseProviders[day].SetProggres(exerciseNumber);
    }
}