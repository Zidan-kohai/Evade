using GeekplaySchool;
using System;
using System.Collections;
using TMPro;
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
        if(!Geekplay.Instance.mobile)
            raisingExplain.SetActive(false);

        raisingPercent.gameObject.SetActive(true);

        raisingPercent.Fill(helpPercent);

        if (raisingCoroutine != null)
            StopCoroutine(raisingCoroutine);

        raisingCoroutine = StartCoroutine(Wait(1f, () =>
        {
            raisingHandler.SetActive(false);
        }));

        if (helpPercent == 1)
        {   
            raisingPercent.Fill(0);
            raisingHandler.SetActive(false);
        }
    }

    #endregion

    #region Speed

    public void ChangeSpeed(float value)
    {
        if(Geekplay.Instance.language == "ru")
        {
            speedValueView.text = $"{value.ToString("F0")}";
        }
        else if (Geekplay.Instance.language == "en")
        {
            speedValueView.text = $"{value.ToString("F0")}";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            speedValueView.text = $"{value.ToString("F0")}";
        }
    }

    #endregion

    #region Deathing

    public void ChangeDeathPercent(float value)
    {
        deathPercent.Fill(value);
        DeathHandler.SetActive(true);

        if (deathCoroutine != null)
            StopCoroutine(deathCoroutine);

        if (!gameObject.activeInHierarchy) return;

        deathCoroutine = StartCoroutine(Wait(1f, () =>
        {
            DeathHandler.SetActive(false);
        }));

        //we can remove logic for showng lose menu here 
    }

    #endregion

    #region Carry
    public void ShowCarryExplain()
    {
        carryExplain.SetActive(true);
    }

    public void DisableCarryExplain()
    {
        carryExplain.SetActive(false);
    }

    public void ShowPutExplain()
    {
        putExplain.SetActive(true);
    }

    public void DisablePutExplain()
    {
        putExplain.SetActive(false);
    }


    #endregion 

    private IEnumerator Wait(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

}
