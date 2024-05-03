using UnityEngine;

public class ChooseMap : MonoBehaviour
{
    [SerializeField] private PlayerIcon playerIcon;
    [SerializeField] private RectTransform easyMapPlayerHandler;
    [SerializeField] private RectTransform middleMapPlayerHandler;
    [SerializeField] private RectTransform hardMapPlayerHandler;

    public void Initialize(int playerCount)
    {
        for(int i = 0; i < playerCount; i++)
        {
            PlayerIcon player = Instantiate(playerIcon, easyMapPlayerHandler);

            player.Initialize(i.ToString());
        }
    }

}
