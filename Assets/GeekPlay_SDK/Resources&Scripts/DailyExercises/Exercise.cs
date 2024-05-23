using System;

[Serializable]
public class Exercise
{
    public int Progress = 0;
    public int MaxProggress = 5;
    public bool IsDone = false;

    public void SetProggres()
    {
        Progress += 1;

        if(Progress >= MaxProggress)
        {
            IsDone = true;
        }
    }
}
