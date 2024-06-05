using GeekplaySchool;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ExperienceShower : MonoBehaviour
{
    [SerializeField] private PlayerExperience experience;
    [SerializeField] private SimpleSlider simpleSlider;
    [SerializeField] private TextMeshProUGUI levelView;
    [SerializeField] private TextMeshProUGUI experienceView;

    private void Start()
    {
        StartCoroutine(Wait(0.3f, Show));
    }

    private void Show()
    {
        levelView.text = $"Level " + experience.GetLevel();

        simpleSlider.Fill(experience.GetFillPercentage());

        experienceView.text = $"{Geekplay.Instance.PlayerData.CurrentExperience}/{Geekplay.Instance.PlayerData.ExperienceToIncreaseLevel}";
    }

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
