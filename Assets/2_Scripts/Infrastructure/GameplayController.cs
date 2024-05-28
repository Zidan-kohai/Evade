using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int playerCount = 0;
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
    }

    public static void AddPlayer(IPlayer player)
    {
        instance.players.Add(player);
        instance.playerCount++;

        player.SubscribeOnDeath(instance.PlayerDeath);
    }

    private void PlayerDeath(IPlayer player)
    {
        playerCount--;

        if(playerCount <= 0)
        {
            End();
        }
    }

    public static void AddRealyPlayer(IPlayer player)
    {
        instance.realyPlayer = player;
        player.SubscribeOnDeath(instance.PlayerDeath);
        instance.playerCount++;
    }

    public void OnPlayerDeath(float livedTime)
    {
        //I know this is a shit
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loseMenu.Show(livedTime);

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
        SceneManager.LoadScene(index);
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
