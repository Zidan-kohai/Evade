using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEndMenu : MonoBehaviour
{
    [SerializeField] private List<PlayerResult> playersResult;
    [SerializeField] private ChooseMap chooseMap;
    [SerializeField] private int playerCount = 0;


    public void Initialize(List<IPlayer> players)
    {
        gameObject.SetActive(true);

        foreach (var player in players)
        {
            AddPlayer(player.GetName(), player.GetHelpCount(), player.GetSurvivedTime(), player.GetEarnedMoney());
        }

        StartCoroutine(Wait(5f, () =>
        {
            gameObject.SetActive(false);
            chooseMap.Initialize(playerCount);
        }));
    }

    public void AddPlayer(string name, int helpedCount, float survivedCount, int earnedMoney)
    {
        foreach(PlayerResult player in playersResult)
        {
            if(!player.handler.activeSelf)
            {
                player.Show(name, helpedCount, survivedCount, earnedMoney);
                playerCount++;
                return;
            }
        }
    }

    private IEnumerator Wait(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action?.Invoke();
    }
}
