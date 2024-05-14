using GeekplaySchool;
using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public event Action<int> LevelUp;
    public event Action<float, float> ChangeExperience;

    [Header("Don`t tauch")]
    [SerializeField] private float currentExperience = 0;
    [SerializeField] private float experienceProgresion = 100;
    [SerializeField] private float experienceToIncreaseLevel = 100;
    [SerializeField] private int level = 1;


    private void Start()
    {
        currentExperience = Geekplay.Instance.PlayerData.CurrentExperience;
        experienceProgresion = Geekplay.Instance.PlayerData.ExperienceProgresion;
        experienceToIncreaseLevel = Geekplay.Instance.PlayerData.ExperienceToIncreaseLevel;
        level = Geekplay.Instance.PlayerData.Level;
    }

    public void SetExperience(float addValue)
    {
        currentExperience += addValue;

        //Check New Level
        if(currentExperience >= experienceToIncreaseLevel)
        {
            currentExperience -= experienceToIncreaseLevel;
            level += 1;
            experienceToIncreaseLevel = experienceProgresion * level;

            Geekplay.Instance.PlayerData.SetExperience(currentExperience, experienceToIncreaseLevel, level);

            LevelUp?.Invoke(level);
        }

        ChangeExperience?.Invoke(currentExperience, experienceToIncreaseLevel);
    }
}
