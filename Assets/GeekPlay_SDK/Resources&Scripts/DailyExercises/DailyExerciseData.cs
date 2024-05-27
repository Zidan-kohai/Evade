using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Daily Exercise/Exercise Data")]
public class DailyExerciseData : ScriptableObject
{
    [SerializeField] private List<ExerciseInfo> exercises = new List<ExerciseInfo>();


    public int ExerciseCount()
    {
        return exercises.Count;
    }

    public ExerciseInfo GetExerciseInfo(int index)
    {
        return exercises[index];
    }
}
