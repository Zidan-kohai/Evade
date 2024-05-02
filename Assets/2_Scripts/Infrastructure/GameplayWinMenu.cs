using System.Collections.Generic;
using UnityEngine;

public class GameplayWinMenu : MonoBehaviour
{
    [SerializeField] private List<PlayerResult> playersResult;
    public void AddPlayer(string name, int helpedCount, float survivedCount, int earnedMoney)
    {
        foreach(PlayerResult player in playersResult)
        {
            if(!player.handler.activeSelf)
            {
                player.Show(name, helpedCount, survivedCount, earnedMoney);
                return;
            }
        }
    }
}
