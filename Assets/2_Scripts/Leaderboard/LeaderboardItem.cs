using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI placeView;
    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI valueView;

    public void Initialize(int place, string name, int value)
    {
        placeView.text = $"{place})";
        nameView.text = name;
        valueView.text = $"{value}";
    }
}