using GeekplaySchool;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int Gold = 1000;
    public int Diamond = 100;
    public float CurrentExperience = 0;
    public float ExperienceProgresion = 100;
    public float ExperienceToIncreaseLevel = 100;
    public int Level = 1;
    public string LastBuy;
    public bool Review;
    public bool IsVolumeOn = true;
    public int HelpCount;
    public int SurviveCount;
    public int DonatCount;
    public bool IsGeometryDashRewardTaked;
    public bool IsCloesChangeRewardTaked;
    public bool IsSlapBattleRewardTaked;
    public bool IsTwoPlayerGameRewardTaked;

    public List<int> BuyedAccessoryID = new List<int>() { 0 };
    public int CurrentEquipedAccessoryID = 0;
    public List<int> BuyedLightID = new List<int>(); 
    public int CurrentEquipedLightID = 0;
    public List<int> BuyedItemID = new List<int>();
    public int CurrentEquipedItemID = 0;
    public List<MyDictionary> BuyedBoosterID = new List<MyDictionary>();
    public List<int> CurrentBoosterKeys = new List<int>() { -1, -1, -1, -1, -1};

    public List<int> Codes = new List<int>();
    #region Exercise Day

    public List<DayExerciseSaveData> exercises = new List<DayExerciseSaveData>();

    #endregion

    public void SetExperience(float currentExperience, float experienceToIncreaseLevel, int level)
    {
        CurrentExperience = currentExperience;
        ExperienceToIncreaseLevel = experienceToIncreaseLevel;
        Level = level;

        Geekplay.Instance.Save();
    }

    public void SetCurrentBoosterKey(int key, ref int cellIndex)
    {
        int index = cellIndex;

        if (CurrentBoosterKeys.Contains(key))
        {
            for (int i = 0; i < CurrentBoosterKeys.Count; i++)
            {
                if (CurrentBoosterKeys[i] == key)
                {
                    CurrentBoosterKeys[i] = -1;
                    cellIndex = i;
                    break;
                }
            }
        }
        else
        {
            cellIndex = -1;
        }

        CurrentBoosterKeys[index] = key;

        Geekplay.Instance.Save();
    }

    public void SetExerciseProgress(Days day, int exerciseNumber, int progress)
    {
        DayExerciseSaveData exerciseSaveData = null;

        foreach (var item in exercises)
        {
            if(item.day == day)
            {
                exerciseSaveData = item;
                break;
            }
        }

        if (exerciseSaveData != null)
        {
            exerciseSaveData.SetExerciseProgress(exerciseNumber, progress);
        }
        else
        {
            exerciseSaveData = new DayExerciseSaveData();
            exerciseSaveData.day = day;
            exerciseSaveData.SetExerciseProgress(exerciseNumber, progress);
            
        }

        exercises.Add(exerciseSaveData);
        Geekplay.Instance.Save();
    }

    
}

[Serializable]
public class DayExerciseSaveData
{
    public Days day;
    public List<ExerciseSaveData> exerciseSaveData;

    public void SetExerciseProgress(int exercise, int progress)
    {
        exerciseSaveData[exercise].exerciseProgress = progress;
    }
}

[Serializable]
public class ExerciseSaveData
{
    public int exerciseProgress;
}

[Serializable]
public class MyDictionary
{
    public int key;
    public int value;
}