using System;
using System.Collections.Generic;

[Serializable]
public class DailyExercise
{
    List<Exercise> exercises = new List<Exercise>();

    public void IsDone()
    {

    }


    //����� ����������� �� 0 �� 1
    public float GetProgress()
    {
        return 0;
    }

    public void SetPrograss(int exerciseNumber)
    {
        exercises[exerciseNumber].SetProggres();
    }
}
