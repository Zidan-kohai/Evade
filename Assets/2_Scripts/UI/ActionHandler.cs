using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    [Header("Raising")]
    [SerializeField] private GameObject raisingHandler;
    [SerializeField] private GameObject raisingExplain;
    [SerializeField] private SimpleSlider raisingPercent;
    private Coroutine raisingCoroutine;

    [Header("Carry")]
    [SerializeField] private GameObject carryHandler;
    [SerializeField] private GameObject carryExplain;
    [SerializeField] private GameObject putExplain;
    private Coroutine carryCoroutine;

    [Header("Speed")]
    [SerializeField] private GameObject speedHandler;
    [SerializeField] private TextMeshProUGUI speedValueView;
    private Coroutine speedCoroutine;

    [Header("Death")]
    [SerializeField] private GameObject DeathHandler;
    [SerializeField] private SimpleSlider deathPercent;
    private Coroutine deathCoroutine;

    #region Help
    public void ShowHelpingUIManual()
    {
        raisingHandler.SetActive(true);
        raisingExplain.SetActive(true);
    }
    public void ShowHelpingUISlider(float percentOfRaising)
    {
        raisingHandler.SetActive(true);
        raisingPercent.gameObject.SetActive(true);

        raisingPercent.Fill(percentOfRaising);
    }

    public void DisableHelpingUIHandler()
    {
        raisingHandler.SetActive(false);
        raisingExplain.SetActive(false);
        raisingPercent.gameObject.SetActive(false);
    }

    public void FilHelpigUI(float helpPercent)
    {
        raisingExplain.SetActive(false);

        raisingPercent.gameObject.SetActive(true);

        raisingPercent.Fill(helpPercent);

        if(helpPercent == 1)
        {   
            raisingPercent.Fill(0);
            raisingHandler.SetActive(false);
        }
    }

    #endregion

    #region Speed

    public void ChangeSpeed(float value)
    {
        speedValueView.text = value.ToString("F0");
    }

    #endregion

    #region Deathing

    public void ChangeDeathPercent(float value)
    {
        deathPercent.Fill(value);
        DeathHandler.SetActive(true);

        if (deathCoroutine != null)
            StopCoroutine(deathCoroutine);

        deathCoroutine = StartCoroutine(Wait(1f, () =>
        {
            DeathHandler.SetActive(false);
        }));

        //we can remove logic for showng lose menu here 
    }

    #endregion


    private IEnumerator Wait(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }
}
