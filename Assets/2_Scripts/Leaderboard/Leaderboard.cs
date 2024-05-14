using DG.Tweening;
using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardType type;
    [SerializeField] private LeaderboardItem surviveLeaderboardItemPrefab;
    [SerializeField] private LeaderboardItem helpLeaderboardItemPrefab;
    [SerializeField] private LeaderboardItem donatLeaderboardItemPrefab;
    [SerializeField] private RectTransform leaderhandler;

    [SerializeField] private Button surviveChapterButton;
    [SerializeField] private Button helpChapterButton;
    [SerializeField] private Button donatChapterButton;
    [SerializeField] private Vector2 selectedSizeDelta;
    [SerializeField] private Vector2 normalSizeDelta;
    private Sequence buttonAnimation;

    [SerializeField] private List<LeaderboardItem> currentLeaders = new List<LeaderboardItem>();

    public void Start()
    {
        surviveChapterButton.onClick.AddListener(OpenSurviveChapter);
        helpChapterButton.onClick.AddListener(OpenHelpChapter);
        donatChapterButton.onClick.AddListener(OpenDonatChapter);

        OpenSurviveChapter();
    }

    private void OpenSurviveChapter()
    {
        if (type == LeaderboardType.Survive) return;

        RectTransform rect = surviveChapterButton.GetComponent<RectTransform>();
        
        buttonAnimation.Kill();

        buttonAnimation = DOTween.Sequence()
            .Append(rect.DOSizeDelta(selectedSizeDelta, 1f)).SetAutoKill(false)
            .OnKill(() => rect.DOSizeDelta(normalSizeDelta, 1f));

        type = LeaderboardType.Survive;

        SwitchLeaderboard(type);
    }

    private void OpenHelpChapter()
    {
        if (type == LeaderboardType.Help) return;

        RectTransform rect = helpChapterButton.GetComponent<RectTransform>();

        buttonAnimation.Kill();

        buttonAnimation = DOTween.Sequence()
            .Append(rect.DOSizeDelta(selectedSizeDelta, 1f)).SetAutoKill(false)
            .OnKill(() => rect.DOSizeDelta(normalSizeDelta, 1f));

        type = LeaderboardType.Help;

        SwitchLeaderboard(type);
    }

    private void OpenDonatChapter()
    {
        if (type == LeaderboardType.Donat) return;

        RectTransform rect = donatChapterButton.GetComponent<RectTransform>();

        buttonAnimation.Kill();

        buttonAnimation = DOTween.Sequence()
            .Append(rect.DOSizeDelta(selectedSizeDelta, 1f)).SetAutoKill(false)
            .OnKill(() => rect.DOSizeDelta(normalSizeDelta, 1f));

        type = LeaderboardType.Donat;

        SwitchLeaderboard(type);
    }

    private void SwitchLeaderboard(LeaderboardType type)
    {
        switch(type)
        {
            case LeaderboardType.Survive:
                Utils.GetLeaderboard("score",0,Helper.SurviveLeaderboardName);
                Utils.GetLeaderboard("name", 0, Helper.SurviveLeaderboardName);
                break;
            case LeaderboardType.Help:
                Utils.GetLeaderboard("score", 0, Helper.HelpLeaderboardName);
                Utils.GetLeaderboard("name", 0, Helper.HelpLeaderboardName);
                break;
            case LeaderboardType.Donat:
                Utils.GetLeaderboard("score", 0, Helper.DonatLeaderboardName);
                Utils.GetLeaderboard("name", 0, Helper.DonatLeaderboardName);
                break;
        }
    }

    public void SetLeadersView(string[] name, string[] value, int count)
    {
        DestroyLeaders();

        for (int i = 0; i < count; i++)
        {
            switch(type)
            {
                case LeaderboardType.Survive:
                    SpawnLeaderboardItem(surviveLeaderboardItemPrefab, name[i], value[i]);
                    break;

                case LeaderboardType.Help:
                    SpawnLeaderboardItem(helpLeaderboardItemPrefab, name[i], value[i]);
                    break;

                case LeaderboardType.Donat:
                    SpawnLeaderboardItem(donatLeaderboardItemPrefab, name[i], value[i]);
                    break;

            }
        }
    }

    private void SpawnLeaderboardItem(LeaderboardItem prefab, string name, string value)
    {
        LeaderboardItem item = Instantiate(prefab, leaderhandler);

        currentLeaders.Add(item);
    }

    private void DestroyLeaders()
    {
        for (int i = 0; i < currentLeaders.Count;i++)
        {
            Destroy(currentLeaders[i].gameObject);
        }
    }

    private enum LeaderboardType
    {
        None,
        Survive,
        Help,
        Donat,
    }
}


