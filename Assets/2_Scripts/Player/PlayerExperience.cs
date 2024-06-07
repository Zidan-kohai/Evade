using GeekplaySchool;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience instance;

    //now it`s unnessary, but if we want to track level up these will be usefull for us
    public event Action<int> LevelUp;
    public event Action<float, float> ChangeExperience;

    [Header("Don`t tauch")]
    [SerializeField] private float currentExperience = 0;
    [SerializeField] private List<float> experienceList;
    [SerializeField] private int level = 1;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }   

        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        currentExperience = Geekplay.Instance.PlayerData.CurrentExperience;
        level = Geekplay.Instance.PlayerData.Level;
    }

    public static void SetExperienceST(float addValue)
    {
        instance.SetExperience(addValue);
    }

    private void SetExperience(float addValue)
    {
        currentExperience += addValue;

        //Check New Level
        if(currentExperience >= experienceList[level])
        {
            level += 1;

            LevelUp?.Invoke(level);
        }


        ChangeExperience?.Invoke(currentExperience, experienceList[level]);

        Geekplay.Instance.PlayerData.SetExperience(currentExperience, experienceList[level], level);
        Geekplay.Instance.Save();
    }

    public string GetLevel()
    {
        return Geekplay.Instance.PlayerData.Level.ToString();
    }

    public float GetFillPercentage()
    {
        return Geekplay.Instance.PlayerData.CurrentExperience / Geekplay.Instance.PlayerData.ExperienceToIncreaseLevel;
    }
}
