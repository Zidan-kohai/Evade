using System;

[Serializable]
public class ExerciseProgress
{
    private int Progress = 0;
    private int MaxProggress = 5;
    private bool IsDone = false;
    public string Description;
    public int Reward;
    public int GetProgress => Progress;

    public int GetMaxProgress => MaxProggress;

    public int SetMaxProgress { set { MaxProggress = value; } }


    public int SetProggres(int value)
    {
        Progress += value;

        if(Progress >= MaxProggress)
        {
            IsDone = true;
        }

        return Progress;
    }
}
