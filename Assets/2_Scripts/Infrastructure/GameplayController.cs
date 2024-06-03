using GeekplaySchool;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    private static GameplayController instance; 

    [SerializeField] private GameplayMainMenu mainMenu;
    [SerializeField] private GameplayLoseMenu loseMenu;
    [SerializeField] private GameplayEndMenu endMenu;
    [SerializeField] private GameObject lookMenu;
    [SerializeField] private float lastedtime;
    [SerializeField] private bool gameOver = false;
    [SerializeField] private IPlayer realyPlayer;
    [SerializeField] private List<IPlayer> players  = new List<IPlayer>();
    [SerializeField] private List<IEnemy> enemies = new List<IEnemy>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (gameOver) return;

        lastedtime -= Time.deltaTime;

        mainMenu.ChangeLostedTime(lastedtime);

        if (lastedtime <= 0)
        {
            End();
        }

        IEnemy nearnestEnemy = GetNearnestEnemyToRealyPlayer();

        if (nearnestEnemy != null)
        {
            PointerManager.Instance.AddToList(nearnestEnemy);
        }
    }

    private IEnemy GetNearnestEnemyToRealyPlayer()
    {
        float minDistance = Mathf.Infinity;
        IEnemy nearnestEnemy = null;

        foreach (IEnemy enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.GetTransform().position, realyPlayer.GetTransform().position);

            if(distance < minDistance)
            {
                minDistance = distance;
                nearnestEnemy = enemy;
            }
        }

        return nearnestEnemy;
    }

    public static void AddPlayerST(IPlayer player)
    {
        instance.players.Add(player);

        player.SubscribeOnFall(instance.OnPlayerFall);
    }

    public static void AddRealyPlayerST(IPlayer player)
    {
        instance.realyPlayer = player;
        player.SubscribeOnFall(instance.OnPlayerFall);
    }

    public static void AddEnemyST(IEnemy enemy)
    {
        instance.enemies.Add(enemy);
    }

    private void OnPlayerFall(IPlayer player)
    {
        foreach (var item in players)
        {
            if(!item.IsFallOrDeath())
            {
                return;
            }
        }

        if (!realyPlayer.IsFallOrDeath()) return;

        End();
    }


    public void OnPlayerDeath(float livedTime)
    {
        //I know this is a shit
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loseMenu.Show(livedTime, realyPlayer.GetEarnedMoney());

        Wallet.AddMoneyST(realyPlayer.GetEarnedMoney());
        PlayerExperience.SetExperienceST(realyPlayer.GetEarnedExperrience());
    }

    public void ShowLookPanel()
    {
        mainMenu.Disable();
        loseMenu.Disable();
        lookMenu.SetActive(true);
    }

    public void LoadScene(int index)
    {
        Geekplay.Instance.ShowInterstitialAd();

        Geekplay.Instance.LoadScene(index);
    }

    private void End()
    {
        gameOver = true;
        Time.timeScale = 0;

        mainMenu.Disable();
        loseMenu.Disable();
        lookMenu.SetActive(false);
        endMenu.Initialize(players, realyPlayer);
    }
}
