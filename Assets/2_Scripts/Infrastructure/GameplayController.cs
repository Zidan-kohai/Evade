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
    }

    public static void AddRealyPlayer(IPlayer player)
    {
        instance.realyPlayer = player;
    }

    public void OnPlayerDeath(float livedTime)
    {
        //I know this is a shit
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loseMenu.Show(livedTime);
    }

    public void ShowLookPanel()
    {
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
