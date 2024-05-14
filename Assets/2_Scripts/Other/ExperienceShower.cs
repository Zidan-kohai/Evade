using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ExperienceShower : MonoBehaviour
{
    [SerializeField] private PlayerExperience experience;
    [SerializeField] private SimpleSlider simpleSlider;
    [SerializeField] private TextMeshProUGUI levelView;

    private void Start()
    {
        StartCoroutine(Wait(0.3f, Show));
    }

    private void Show()
    {
        levelView.text = $"Level " + experience.GetLevel();

        simpleSlider.Fill(experience.GetFillPercentage());
    }

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
