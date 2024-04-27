using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private GameplayMainMenu mainMenu;

    [SerializeField] private float lastedtime;

    private void Update()
    {
        lastedtime -= Time.deltaTime;

        mainMenu.ChangeLostedTime(lastedtime);
    }
}
