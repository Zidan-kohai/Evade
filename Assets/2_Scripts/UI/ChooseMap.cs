using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMap : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeView;
    [SerializeField] private float timeToChooseMap;
    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private PlayerIcon playerIconPrefab;
    [SerializeField] private RectTransform easyMapPlayerHandler;
    [SerializeField] private RectTransform middleMapPlayerHandler;
    [SerializeField] private RectTransform hardMapPlayerHandler;
    [SerializeField] private RectTransform exitMapPlayerHandler;

    [SerializeField] private Button easyMap;
    [SerializeField] private Button middleMap;
    [SerializeField] private Button hardMap;
    [SerializeField] private Button exitMap;

    [SerializeField] private PlayerIcon playerIcon;
    [SerializeField] private List<PlayerIcon> AIPlayersIcon;

    public void Initialize(int playerCount)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        SpawnPlayerIcon(playerCount);

        AddEventToButton();

        StartCoroutine(Wait());
    }

    private void DecideWhichMapOpen()
    {
        if(playerIcon.GetMapID == 4)
        {
            sceneLoader.LoadScene(0);
            return;
        }
        //there should be a logic in deciding which card to launch
    }

    private void AddEventToButton()
    {
        easyMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(easyMapPlayerHandler, 1);
        });

        middleMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(middleMapPlayerHandler, 2);
        });

        hardMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(hardMapPlayerHandler, 3);
        });

        exitMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(exitMapPlayerHandler, 4);
        });
    }

    private void SpawnPlayerIcon(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            PlayerIcon player = Instantiate(playerIconPrefab, easyMapPlayerHandler);
            if (i == 0)
            {
                playerIcon = player;
                player.Initialize("Вы");
            }
            else
            {
                AIPlayersIcon.Add(player);
                player.Initialize(i.ToString());
            }
        }
    }

    private IEnumerator Wait()
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
}
