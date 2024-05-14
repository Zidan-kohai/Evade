using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    private static BuffHandler instance;

    [SerializeField] private List<Enemy> enemies = new List<Enemy>();

    [SerializeField] private IRealyPlayer realyPlayer;

    private void Awake()
    {
        instance = this;
    }

    #region Player
    public static void AddRealyPlayerST(IRealyPlayer player)
    {
        instance.AddRealyPlayer(player);
    }

    private void AddRealyPlayer(IRealyPlayer player)
    {
        realyPlayer = player;
    }

    #endregion

    public void HeadphoneBuff()
    {
        foreach (Enemy enemy in enemies) 
        {
            enemy.IncreaseSoundZone();
        }
    }

    public void BadgeBuff()
    {
        realyPlayer.SetExperrienceMulltiplierFactor(2);
    }

    public void MoneyPortfolioBuff()
    {
        realyPlayer.SetMoneyMulltiplierFactor(2);
    }
}
