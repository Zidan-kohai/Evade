using System;
using Unity.VisualScripting;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    [Header("Raising")]
    [SerializeField] private GameObject raisingHandler;
    [SerializeField] private GameObject raisingExplain;
    [SerializeField] private SimpleSlider raisingPercent;

    [Header("Carry")]
    [SerializeField] private GameObject carryHandler;
    [SerializeField] private GameObject carryExplain;
    [SerializeField] private GameObject putExplain;


    #region Help

    public void CanHelp(float percentOfRaising)
    {
        raisingHandler.SetActive(true);
        raisingExplain.SetActive(true);

        raisingPercent.Fill(percentOfRaising);
    }

    public void CannotHelp()
    {
        raisingHandler.SetActive(false);
        raisingExplain.SetActive(false);
    }

    public void Help(float helpPercent)
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
}
