using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyExercise
{
    [SerializeField] private List<Exercise> exercises = new List<Exercise>();

    public void IsDone()
    {

    }


    //Метод возвраащает от 0 до 1
    public float GetProgress()
    {
        return 0;
    }

    public void SetPrograss(int exerciseNumber)
    {
        exercises[exerciseNumber].SetProggres();
    }
}
