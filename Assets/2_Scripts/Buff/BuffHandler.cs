using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    private static BuffHandler instance;

    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private List<IPlayer> players = new List<IPlayer>();
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

    public static void AddPlayerST(IPlayer player)
    {
        instance.AddPlayer(player);
    }

    private void AddPlayer(IPlayer player)
    {
        players.Add(player);
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
        realyPlayer.SetExperienceMulltiplierFactor(2);
    }

    public void MoneyPortfolioBuff()
    {
        realyPlayer.SetMoneyMulltiplierFactor(2);
    }

    public void StethoscopeBuff()
    {
        foreach (var item in players)
        {
            item.SetTimeToUp(2);
        }
    }

    public void HelthKitBuff()
    {
        realyPlayer.SetMaxSpeedOnFall(20, false);
    }

    public void StiltsKitBuff()
    {
        realyPlayer.SetMaxSpeedOnUp(10);
    }

    public void HermesShoes()
    {
        realyPlayer.SetMaxSpeedOnUp(80);
    }
}
