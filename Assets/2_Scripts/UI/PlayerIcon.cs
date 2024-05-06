using TMPro;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI idView;
    [SerializeField] private int id;
    [SerializeField] private RectTransform rect;
    [SerializeField] private PlayerIconHandler handler;

    public int GetMapID => handler.MapId;

    public void Initialize(int id, string name)
    {
        this.id = id;
        idView.text = id.ToString();
    }

    public void SetMap(PlayerIconHandler iconHandler)
    {
        handler?.RemovePlayer(id);

        rect.parent = iconHandler.Handler;

        handler = iconHandler;

        handler.AddPlayer(id);
    }
}
