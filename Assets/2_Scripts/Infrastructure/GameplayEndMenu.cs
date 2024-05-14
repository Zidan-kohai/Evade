using GeekplaySchool;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayEndMenu : MonoBehaviour
{
    [SerializeField] private List<PlayerResult> playersResult;
    [SerializeField] private ChooseMapSystem chooseMap;
    [SerializeField] private TextMeshProUGUI HeaderTextView;
    [SerializeField] private int playerCount = 0;


    public void Initialize(List<IPlayerInfo> players, IPlayerInfo realyPlayer)
    {
        gameObject.SetActive(true);

        AddPlayer(realyPlayer.GetName(), realyPlayer.GetHelpCount(), realyPlayer.GetSurvivedTime(), realyPlayer.GetEarnedMoney(), realyPlayer.GetEarnedExperrience());

        Wallet.AddMoneyST(realyPlayer.GetEarnedMoney(), 0);
        PlayerExperience.SetExperienceST(realyPlayer.GetEarnedExperrience());

        if(realyPlayer.IsDeath())
        {
            HeaderTextView.text = "Вы мертвы";
        }
        else
        {
            HeaderTextView.text = "Вы выжили";

            Geekplay.Instance.PlayerData.SurviveCount++;
            Geekplay.Instance.SetLeaderboard(Helper.SurviveLeaderboardName, Geekplay.Instance.PlayerData.SurviveCount);
            Geekplay.Instance.Save();

        }


        foreach (var player in players)
        {
            AddPlayer(player.GetName(), player.GetHelpCount(), player.GetSurvivedTime(), player.GetEarnedMoney(), player.GetEarnedExperrience());
        }

        Close();
        
    }


    public void AddPlayer(string name, int helpedCount, float survivedCount, int earnedMoney, int experience)
    {
        foreach(PlayerResult player in playersResult)
        {
            if(!player.handler.activeSelf)
            {
                player.Show(name, helpedCount, survivedCount, earnedMoney, experience);
                playerCount++;
                return;
            }
        }
    }

    private void Close()
    {
        StartCoroutine(Wait(5f, () =>
        {
            gameObject.SetActive(false);
            chooseMap.Initialize(playerCount);
        }));
    }

    private IEnumerator Wait(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action?.Invoke();
    }
}
