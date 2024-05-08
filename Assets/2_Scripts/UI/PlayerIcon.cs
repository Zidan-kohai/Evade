using TMPro;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI idView;
    [SerializeField] private int id;
    [SerializeField] private RectTransform rect;
    [SerializeField] private PlayerIconHandler handler;

    public int GetMapID => handler.GetMapID;

    public void Initialize(int id, string name)
    {
        this.id = id;
        idView.text = id.ToString();
    }

    public void SetMap(PlayerIconHandler iconHandler)
    {
        handler?.RemovePlayer(id);

        handler = iconHandler;

        handler.AddPlayer(rect, id);
    }
}
