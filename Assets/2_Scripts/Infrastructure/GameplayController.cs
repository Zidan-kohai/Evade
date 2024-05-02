using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    private static GameplayController instance; 

    [SerializeField] private GameplayMainMenu mainMenu;
    [SerializeField] private GameplayLoseMenu loseMenu;
    [SerializeField] private GameplayWinMenu winMenu;
    [SerializeField] private GameObject lookMenu;
    [SerializeField] private float lastedtime;
    private bool gameOver = false;
    [SerializeField] List<IPlayer> players = new List<IPlayer>();

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
            Win();
        }
    }

    public static void AddPlayer(IPlayer player)
    {
        instance.players.Add(player);
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

    private void Win()
    {
        gameOver = true;
        Time.timeScale = 0;
        winMenu.gameObject.SetActive(true);
        foreach (var player in players)
        {
            winMenu.AddPlayer(player.GetName(), player.GetHelpCount(), player.GetSurvivedTime(), player.GetEarnedMoney());
        }
    }

}
