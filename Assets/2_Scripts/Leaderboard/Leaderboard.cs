using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private RectTransform leaderhandler;
    [SerializeField] private LeaderboardItem leaderboardItemPrefab;

    [SerializeField] private List<LeaderboardItem> currentLeaders = new List<LeaderboardItem>();

    public void SetLeadersView(string[] name, string[] value, int count)
    {
        DestroyLeaders();

        for (int i = 0; i < count; i++)
        {
            Debug.Log("Name: " + name[i]);
            Debug.Log("Value: " + value[i]);


            SpawnLeaderboardItem(leaderboardItemPrefab, name[i], value[i], i);
        }
    }

    private void SpawnLeaderboardItem(LeaderboardItem prefab, string name, string value, int place)
    {
        LeaderboardItem item = Instantiate(prefab, leaderhandler);
        item.Initialize(place + 1, name, value);
        currentLeaders.Add(item);
    }

    private void DestroyLeaders()
    {
        Geekplay.Instance.lN.Clear();
        Geekplay.Instance.l.Clear();

        for (int i = 0; i < currentLeaders.Count;i++)
        {
            LeaderboardItem leaderboardItem = currentLeaders[i];
            Destroy(leaderboardItem.gameObject);
        }

        currentLeaders.Clear();
    }
}


