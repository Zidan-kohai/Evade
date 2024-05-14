using GeekplaySchool;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience instance;

    //now it`s unnessary, but if we want to track level up these will be usefull for us
    public event Action<int> LevelUp;
    public event Action<float, float> ChangeExperience;

    [Header("Don`t tauch")]
    [SerializeField] private float currentExperience = 0;
    [SerializeField] private float experienceProgresion = 100;
    [SerializeField] private float experienceToIncreaseLevel = 100;
    [SerializeField] private int level = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentExperience = Geekplay.Instance.PlayerData.CurrentExperience;
        experienceProgresion = Geekplay.Instance.PlayerData.ExperienceProgresion;
        experienceToIncreaseLevel = Geekplay.Instance.PlayerData.ExperienceToIncreaseLevel;
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
        if(currentExperience >= experienceToIncreaseLevel)
        {
            currentExperience -= experienceToIncreaseLevel;
            level += 1;
            experienceToIncreaseLevel = experienceProgresion * level;

            LevelUp?.Invoke(level);
        }


        ChangeExperience?.Invoke(currentExperience, experienceToIncreaseLevel);

        Geekplay.Instance.PlayerData.SetExperience(currentExperience, experienceToIncreaseLevel, level);
        Geekplay.Instance.Save();
    }
}
