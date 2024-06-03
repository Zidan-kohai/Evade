using GeekplaySchool;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMapSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeView;
    [SerializeField] private TextMeshProUGUI descriptionView;
    [SerializeField] private float timeToChooseMap;

    [SerializeField] private List<PlayerIcon> playerIcons;
    [SerializeField] private PlayerIconHandler easyMapPlayerHandler;
    [SerializeField] private PlayerIconHandler middleMapPlayerHandler;
    [SerializeField] private PlayerIconHandler hardMapPlayerHandler;
    [SerializeField] private PlayerIconHandler exitMapPlayerHandler;

    [SerializeField] private Button easyMap;
    [SerializeField] private Button middleMap;
    [SerializeField] private Button hardMap;
    [SerializeField] private Button exitMap;

    [SerializeField] private float minTime = 0.5f, maxTime = 2f;

    private PlayerIcon playerIcon;
    private List<PlayerIcon> AIPlayersIcon = new List<PlayerIcon>();
    private int playersCount;


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

        descriptionView.text = easyMapPlayerHandler.GetDescription;
    }

    private void DecideWhichMapOpen()
    {
        if(playerIcon.GetMapID == 0)
        {
            Geekplay.Instance.LoadScene(playerIcon.GetMapID);
            return;
        }
        //there should be a logic for deciding which card to launch

        int mapIndex = 0;
        int voiseCount = 0;

        if(voiseCount < easyMapPlayerHandler.GetVoiseCount)
        {
            mapIndex = easyMapPlayerHandler.GetMapID;
            voiseCount = easyMapPlayerHandler.GetVoiseCount;
        }
        if (voiseCount < middleMapPlayerHandler.GetVoiseCount)
        {
            mapIndex = middleMapPlayerHandler.GetMapID;
            voiseCount = middleMapPlayerHandler.GetVoiseCount;
        }
        if (voiseCount < hardMapPlayerHandler.GetVoiseCount)
        {
            mapIndex = hardMapPlayerHandler.GetMapID;
            voiseCount = hardMapPlayerHandler.GetVoiseCount;
        }


        Debug.Log("mapIndex: " + mapIndex);
        Debug.Log("voiseCount: " + voiseCount);


        Geekplay.Instance.LoadScene(mapIndex);
    }

    private void AddEventToButton()
    {
        easyMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(easyMapPlayerHandler);
            descriptionView.text = easyMapPlayerHandler.GetDescription;
        });

        middleMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(middleMapPlayerHandler);
            descriptionView.text = middleMapPlayerHandler.GetDescription;
        });

        hardMap.onClick.AddListener(() =>
        {
            playerIcon.SetMap(hardMapPlayerHandler);
            descriptionView.text = hardMapPlayerHandler.GetDescription;
        });

        exitMap.onClick.AddListener(() =>
        {
            Geekplay.Instance.LoadScene(exitMapPlayerHandler.GetMapID);
        });
    }

    private void SpawnPlayerIcon(int playerCount)
    {
        for (int i = 0; i < playerIcons.Count; i++)
        {
            if(i < playerCount)
            {
                playerIcon = playerIcons[i];

                playerIcon.gameObject.SetActive(true);

                if (i == 0)
                {
                    if (Geekplay.Instance.language == "ru")
                    {
                        playerIcon.Initialize(i, "Вы");
                    }
                    else if (Geekplay.Instance.language == "en")
                    {
                        playerIcon.Initialize(i, "You");
                    }
                    else if (Geekplay.Instance.language == "tr")
                    {
                        playerIcon.Initialize(i, "Sen");
                    }
                }
                else
                {
                    AIPlayersIcon.Add(playerIcons[i]);
                    playerIcons[i].Initialize(i, i.ToString());
                    playerIcons[i].SetMap(easyMapPlayerHandler);
                }
            }
            else
            {
                playerIcons[i].gameObject.SetActive(false);
            }

        }
    }
    private void AIMove(PlayerIcon icon, int realyPlayerMapId, int playersCountAvarage)
    {
        if (easyMapPlayerHandler.GetMapID == realyPlayerMapId
            && easyMapPlayerHandler.GetVoiseCount < playersCountAvarage)
        {
            icon.SetMap(easyMapPlayerHandler);
        }
        else if (middleMapPlayerHandler.GetMapID == realyPlayerMapId
            && middleMapPlayerHandler.GetVoiseCount < playersCountAvarage)
        {
            icon.SetMap(middleMapPlayerHandler);
        }
        else if (hardMapPlayerHandler.GetMapID == realyPlayerMapId
            && hardMapPlayerHandler.GetVoiseCount < playersCountAvarage)
        {
            icon.SetMap(hardMapPlayerHandler);
        }
        else if (exitMapPlayerHandler.GetMapID == realyPlayerMapId
            && exitMapPlayerHandler.GetVoiseCount < playersCountAvarage)
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
            }
        }
    }

    private IEnumerator WaitToClose()
    {
        while(timeToChooseMap > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            timeToChooseMap -= 1f;

            timeView.text = FormatSeconds(timeToChooseMap);
        }

        DecideWhichMapOpen();
    }
    public string FormatSeconds(float seconds)
    {
        int totalSeconds = Mathf.FloorToInt(seconds);
        int minutes = totalSeconds / 60;
        int remainingSeconds = totalSeconds % 60;
        return $"{minutes:D2}:{remainingSeconds:D2}";
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