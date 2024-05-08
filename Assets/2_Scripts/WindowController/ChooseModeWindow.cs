using GeekplaySchool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChooseModeWindow : Window
{
    [SerializeField] private TextMeshProUGUI mapDescription;
    [SerializeField] private MapItem defaultMap;

    [SerializeField] private Button playButton;

    private void Awake()
    {
        ChangeSelectedMode(defaultMap);
    }

    public void ChangeSelectedMode(MapItem Item)
    {
        playButton.onClick.RemoveAllListeners();
        
        mapDescription.text = Item.GetDescription;

        playButton.onClick.AddListener(() =>
        {
            Geekplay.Instance.LoadScene(Item.GetSceneIndex);
        });
    }
}
