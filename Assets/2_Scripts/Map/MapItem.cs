using GeekplaySchool;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MapItemData data;
    [SerializeField] private ChooseModeWindow chooseModeWindow;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI hardnest;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image iconImage;

    public string GetDescription => data.GetDescription();

    public int GetSceneIndex => data.SceneIndex;

    private void Start()
    {
        ShowInfo();

        button.onClick.AddListener(() =>
        {
            Geekplay.Instance.LoadScene(data.SceneIndex);
        });
    }

    private void ShowInfo()
    {
        iconImage.sprite = data.Icon;

        name.text = data.GetName();
        hardnest.text = data.GetHardnest();
        description.text = data.GetDescription();
    }
}
