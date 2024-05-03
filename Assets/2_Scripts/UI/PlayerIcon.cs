using TMPro;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI id;
    [SerializeField] private RectTransform rect;
    [SerializeField] private int mapId;

    public int GetMapID => mapId;

    public void Initialize(string id)
    {
        this.id.text = id;
    }

    public void SetMap(RectTransform mapRect, int mapId)
    {
        rect.parent = mapRect;
        this.mapId = mapId;
    }
}
