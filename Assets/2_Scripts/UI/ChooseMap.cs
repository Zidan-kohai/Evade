using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMap : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeView;
    [SerializeField] private float timeToChooseMap;
    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private PlayerIcon playerIconPrefab;
    [SerializeField] private PlayerIconHandler easyMapPlayerHandler;
    [SerializeField] private PlayerIconHandler middleMapPlayerHandler;
    [SerializeField] private PlayerIconHandler hardMapPlayerHandler;
    [SerializeField] private PlayerIconHandler exitMapPlayerHandler;

    [SerializeField] private Button easyMap;
    [SerializeField] private Button middleMap;
    [SerializeField] private Button hardMap;
    [SerializeField] private Button exitMap;

    [SerializeField] private PlayerIcon playerIcon;
    [SerializeField] private List<PlayerIcon> AIPlayersIcon;
    [SerializeField] private int playersCount;

    [SerializeField] private float minTime = 0.5f, maxTime = 2f;

    public void Initialize(int playerCount)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        SpawnPlayerIcon(playerCount);

        AddEventToButton();

        StartCoroutine(WaitToClose());
        playersCount = playerCount;

        for (int i = 0; i < AIPlayersIcon.Count; i++)
        {
            StartCoroutine(AIChooseMap(AIPlayersIcon[i]));
        }
    }

    private void DecideWhichMapOpen()
    {
        if(playerIcon.GetMapID == 4)
        {
            sceneLoader.LoadScene(0);
            return;
        }
        //there should be a logic in deciding which card to launch

        sceneLoader.LoadScene(1);
    }

    private void AddEventToButton()
    {
        easyMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(easyMapPlayerHandler);
        });

        middleMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(middleMapPlayerHandler);
        });

        hardMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(hardMapPlayerHandler);
        });

        exitMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(exitMapPlayerHandler);
        });
    }

    private void SpawnPlayerIcon(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            PlayerIcon player = Instantiate(playerIconPrefab);

            player.SetMap(easyMapPlayerHandler);

            if (i == 0)
            {
                playerIcon = player;
                player.Initialize(i, "Вы");
            }
            else
            {
                AIPlayersIcon.Add(player);
                player.Initialize(i, i.ToString());
            }
        }
    }
    private void AIMove(PlayerIcon icon, int realyPlayerMapId, int playersCountAvarage)
    {
        if (easyMapPlayerHandler.MapId == realyPlayerMapId
            && easyMapPlayerHandler.playersIndex.Count < playersCountAvarage)
        {
            icon.SetMap(easyMapPlayerHandler);
        }
        else if (middleMapPlayerHandler.MapId == realyPlayerMapId
            && middleMapPlayerHandler.playersIndex.Count < playersCountAvarage)
        {
            icon.SetMap(middleMapPlayerHandler);
        }
        else if (hardMapPlayerHandler.MapId == realyPlayerMapId
            && hardMapPlayerHandler.playersIndex.Count < playersCountAvarage)
        {
            icon.SetMap(hardMapPlayerHandler);
        }
        else if (exitMapPlayerHandler.MapId == realyPlayerMapId
            && exitMapPlayerHandler.playersIndex.Count < playersCountAvarage)
        {
            icon.SetMap(exitMapPlayerHandler);
        }
        else
        {
            int mapId = UnityEngine.Random.Range(0, 4);
            switch(mapId)
            {
                case 0:
                    icon.SetMap(easyMapPlayerHandler);
                    break;
                case 1:
                    icon.SetMap(middleMapPlayerHandler);
                    break;
                case 2:
                    icon.SetMap(hardMapPlayerHandler);
                    break;
                case 3:
                    icon.SetMap(exitMapPlayerHandler); 
                    break;
            }
        }
    }

    private IEnumerator WaitToClose()
    {
        while(timeToChooseMap > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            timeToChooseMap -= 1f;
            TimeSpan time = TimeSpan.FromSeconds(timeToChooseMap);
            timeView.text = $"{time.Minutes} : {time.Seconds}";
        }

        DecideWhichMapOpen();
    }

    private IEnumerator AIChooseMap(PlayerIcon icon)
    {
        float time = UnityEngine.Random.Range(minTime, maxTime);
        int playersCountAvarage = playersCount / 2;

        while (true)
        {
            yield return new WaitForSecondsRealtime(time);

            int realyPlayerMapId = playerIcon.GetMapID;

            AIMove(icon, realyPlayerMapId, playersCountAvarage);
        }
    }

}