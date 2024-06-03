using GeekplaySchool;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayEndMenu : MonoBehaviour
{
    [SerializeField] private List<PlayerResult> playersResult;
    [SerializeField] private ChooseMapSystem chooseMap;
    [SerializeField] private TextMeshProUGUI HeaderTextView;
    [SerializeField] private int playerCount = 0;
    [SerializeField] private Color youDiedColor;

    public void Initialize(List<IPlayer> players, IPlayer realyPlayer)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        AddPlayer(realyPlayer.GetName(), realyPlayer.GetHelpCount(), realyPlayer.GetSurvivedTime(), realyPlayer.GetEarnedMoney(), realyPlayer.GetEarnedExperrience());

        if(realyPlayer.IsFallOrDeath())
        {
            if (Geekplay.Instance.language == "ru")
            {
                HeaderTextView.text = "Вы мертвы";
            }
            else if (Geekplay.Instance.language == "en")
            {
                HeaderTextView.text = "you are dead";
            }
            else if (Geekplay.Instance.language == "tr")
            {
                HeaderTextView.text = "sen olusun";
            }

            HeaderTextView.color = youDiedColor;
        }
        else
        {
            if(Geekplay.Instance.language == "ru")
            {
                HeaderTextView.text = "Вы выжили";
            }
            else if(Geekplay.Instance.language == "en")
            {
                HeaderTextView.text = "You survived";
            }
            else if(Geekplay.Instance.language == "tr")
            {
                HeaderTextView.text = "hayatta kaldin";
            }

            Geekplay.Instance.PlayerData.SurviveCount++;

            DailyExerciseController.Instance.SetProgress(Days.Day3, 1);
            DailyExerciseController.Instance.SetProgress(Days.Day5, 0);

            if (SceneManager.GetActiveScene().name == "Building")
            {
                DailyExerciseController.Instance.SetProgress(Days.Day2, 0);
            }
            if (SceneManager.GetActiveScene().name == "Mansion")
            {
                DailyExerciseController.Instance.SetProgress(Days.Day4, 1);
            }
            Geekplay.Instance.SetLeaderboard(Helper.SurviveLeaderboardName, Geekplay.Instance.PlayerData.SurviveCount);
            Geekplay.Instance.Save();

        }


        foreach (var player in players)
        {
            AddPlayer(player.GetName(), player.GetHelpCount(), player.GetSurvivedTime(), player.GetEarnedMoney(), player.GetEarnedExperrience());
        }
        
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

    public void Close()
    {
        Geekplay.Instance.ShowInterstitialAd();

        gameObject.SetActive(false);
        chooseMap.Initialize(playerCount);
    }
}
