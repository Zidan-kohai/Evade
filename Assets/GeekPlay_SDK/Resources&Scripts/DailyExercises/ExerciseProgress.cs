using System;

[Serializable]
public class ExerciseProgress
{
    private int progress = 0;
    private int maxProggress = 5;
    private bool isDone = false;
    public string Description;
    public int Reward;
    public int GetProgress => progress;

    public int GetMaxProgress => maxProggress;

    public int SetMaxProgress { set { maxProggress = value; } }

    public bool IsDone => isDone;
        
    public int SetProggres(int value)
    {
        if (isDone) return progress;

        progress += value;

        if(progress >= maxProggress)
        {
            isDone = true;
        }

        return progress;
    }
}
