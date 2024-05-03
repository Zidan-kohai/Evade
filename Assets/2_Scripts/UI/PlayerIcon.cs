using TMPro;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI id;
    [SerializeField] private RectTransform rect;

    public void Initialize(string id)
    {
        this.id.text = id;
    }

    public void SetMap(RectTransform mapRect)
    {
        rect.parent = mapRect;
    }
}
