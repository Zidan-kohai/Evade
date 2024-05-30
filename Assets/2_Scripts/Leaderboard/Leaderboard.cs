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
    [SerializeField] private Image surviveChapterButtonImage;
    [SerializeField] private Button helpChapterButton;
    [SerializeField] private Image helpChapterButtonImage;
    [SerializeField] private Button donatChapterButton;
    [SerializeField] private Image donatChapterButtonImage;
    [SerializeField] private Vector2 selectedSizeDelta;
    [SerializeField] private Vector2 normalSizeDelta;
    private Sequence buttonAnimation;

    [SerializeField] private List<LeaderboardItem> currentLeaders = new List<LeaderboardItem>();

    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    public void Start()
    {
        Geekplay.Instance.leaderboard = this;

        surviveChapterButton.onClick.AddListener(OpenSurviveChapter);
        helpChapterButton.onClick.AddListener(OpenHelpChapter);
        donatChapterButton.onClick.AddListener(OpenDonatChapter);

        OpenSurviveChapter();
    }

    private void OpenSurviveChapter()
    {
        if (type == LeaderboardType.Survive) return;

        surviveChapterButtonImage.sprite = openSprite;
        helpChapterButtonImage.sprite = closeSprite;
        donatChapterButtonImage.sprite = closeSprite;

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

        surviveChapterButtonImage.sprite = closeSprite;
        helpChapterButtonImage.sprite = openSprite;
        donatChapterButtonImage.sprite = closeSprite;

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

        surviveChapterButtonImage.sprite = closeSprite;
        helpChapterButtonImage.sprite = closeSprite;
        donatChapterButtonImage.sprite = openSprite;

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
        if (Geekplay.Instance.Platform == Platform.Editor)
        {
            SetLeadersView(Geekplay.Instance.lN.ToArray(), Geekplay.Instance.l.ToArray(), Geekplay.Instance.lN.Count);
            return;
        }

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
            Debug.Log("Name: " + name[i]);
            Debug.Log("Value: " + value[i]);

            switch (type)
            {
                case LeaderboardType.Survive:
                    SpawnLeaderboardItem(surviveLeaderboardItemPrefab, name[i], value[i], i);
                    break;

                case LeaderboardType.Help:
                    SpawnLeaderboardItem(helpLeaderboardItemPrefab, name[i], value[i], i);
                    break;

                case LeaderboardType.Donat:
                    SpawnLeaderboardItem(donatLeaderboardItemPrefab, name[i], value[i], i);
                    break;

            }
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

    private enum LeaderboardType
    {
        None,
        Survive,
        Help,
        Donat,
    }
}


