using DG.Tweening;
using GeekplaySchool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardType type;
    [SerializeField] private Leaderboard surviveLeaderboard;
    [SerializeField] private Leaderboard helpLeaderboard;
    [SerializeField] private Leaderboard donatLeaderboard;

    [SerializeField] private Button surviveChapterButton;
    [SerializeField] private Image surviveChapterButtonImage;
    [SerializeField] private Button helpChapterButton;
    [SerializeField] private Image helpChapterButtonImage;
    [SerializeField] private Button donatChapterButton;
    [SerializeField] private Image donatChapterButtonImage;
    [SerializeField] private Vector2 selectedSizeDelta;
    [SerializeField] private Vector2 normalSizeDelta;
    private Sequence buttonAnimation; 
    
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    [SerializeField] private TextMeshProUGUI remainingTimeToUpdaeLeaderboardTextView; 

    private void Start()
    {
        Geekplay.Instance.leaderboard = this;

        surviveChapterButton.onClick.AddListener(OpenSurviveChapter);
        helpChapterButton.onClick.AddListener(OpenHelpChapter);
        donatChapterButton.onClick.AddListener(OpenDonatChapter);

        StartGetLeaderboard(); 
        OpenSurviveChapter();
    }

    private void Update()
    {
        remainingTimeToUpdaeLeaderboardTextView.text = string.Format("{0:f0}", Geekplay.Instance.remainingTimeUntilUpdateLeaderboard);

        if(Geekplay.Instance.remainingTimeUntilUpdateLeaderboard <= 0)
        {
            Geekplay.Instance.remainingTimeUntilUpdateLeaderboard = Geekplay.Instance.timeToUpdateLeaderboard;

            StartGetLeaderboard();
        }
    }

    private void StartGetLeaderboard()
    {
        if (Geekplay.Instance.Platform == Platform.Editor) return;

        Utils.GetLeaderboard("score", 0, Helper.SurviveLeaderboardName);
        Utils.GetLeaderboard("name", 0, Helper.SurviveLeaderboardName);
    }

    public void OpenSurviveChapter()
    {
        ClosePreviosLeaderboard(type);

        surviveLeaderboard.gameObject.SetActive(true);

        surviveChapterButtonImage.sprite = openSprite;
        helpChapterButtonImage.sprite = closeSprite;
        donatChapterButtonImage.sprite = closeSprite;

        RectTransform rect = surviveChapterButton.GetComponent<RectTransform>();

        buttonAnimation.Kill();

        buttonAnimation = DOTween.Sequence()
            .Append(rect.DOSizeDelta(selectedSizeDelta, 1f)).SetAutoKill(false)
            .OnKill(() => rect.DOSizeDelta(normalSizeDelta, 1f));

        type = LeaderboardType.Survive;

    }

    private void CloseSurviveChapter()
    {
        surviveLeaderboard.gameObject.SetActive(false);
    }

    public void OpenHelpChapter()
    {
        ClosePreviosLeaderboard(type);

        helpLeaderboard.gameObject.SetActive(true);

        surviveChapterButtonImage.sprite = closeSprite;
        helpChapterButtonImage.sprite = openSprite;
        donatChapterButtonImage.sprite = closeSprite;

        RectTransform rect = helpChapterButton.GetComponent<RectTransform>();

        buttonAnimation.Kill();

        buttonAnimation = DOTween.Sequence()
            .Append(rect.DOSizeDelta(selectedSizeDelta, 1f)).SetAutoKill(false)
            .OnKill(() => rect.DOSizeDelta(normalSizeDelta, 1f));

        type = LeaderboardType.Help;

    }

    private void CloseHelpChapter()
    {
        helpLeaderboard.gameObject.SetActive(false);
    }

    public void OpenDonatChapter()
    {
        ClosePreviosLeaderboard(type);
        donatLeaderboard.gameObject.SetActive(true);

        surviveChapterButtonImage.sprite = closeSprite;
        helpChapterButtonImage.sprite = closeSprite;
        donatChapterButtonImage.sprite = openSprite;

        RectTransform rect = donatChapterButton.GetComponent<RectTransform>();

        buttonAnimation.Kill();

        buttonAnimation = DOTween.Sequence()
            .Append(rect.DOSizeDelta(selectedSizeDelta, 1f)).SetAutoKill(false)
            .OnKill(() => rect.DOSizeDelta(normalSizeDelta, 1f));

        type = LeaderboardType.Donat;

    }
    private void CloseDonatChapter()
    {
        donatLeaderboard.gameObject.SetActive(false);
    }

    private void ClosePreviosLeaderboard(LeaderboardType type)
    {
        switch (type)
        {
            case LeaderboardType.Survive:
                CloseSurviveChapter();
                break;
            case LeaderboardType.Help:
                CloseHelpChapter();
                break;
            case LeaderboardType.Donat:
                CloseDonatChapter();
                break;
        }
    }

    public void SetLeadersView(string[] names, string[] values, int count, string leaderboardName)
    {
        if (leaderboardName == "Survive")
        {
            surviveLeaderboard.SetLeadersView(names, values, count);

            Utils.GetLeaderboard("score", 0, Helper.HelpLeaderboardName);
            Utils.GetLeaderboard("name", 0, Helper.HelpLeaderboardName);
        }
        else if (leaderboardName == "Help")
        {
            helpLeaderboard.SetLeadersView(names, values, count);
            Utils.GetLeaderboard("score", 0, Helper.DonatLeaderboardName);
            Utils.GetLeaderboard("name", 0, Helper.DonatLeaderboardName);
        }
        else if (leaderboardName == "Donat")
        {
            donatLeaderboard.SetLeadersView(names, values, count);
        }
    }

    private enum LeaderboardType
    {
        Survive,
        Help,
        Donat,
    }
}
