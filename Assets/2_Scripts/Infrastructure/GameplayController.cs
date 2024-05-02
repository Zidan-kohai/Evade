using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private GameplayMainMenu mainMenu;
    [SerializeField] private GameplayLoseMenu loseMenu;
    [SerializeField] private GameObject cameraSwitherHandler;
    [SerializeField] private GameObject lookMenu;
    [SerializeField] private float lastedtime;

    private void Update()
    {
        lastedtime -= Time.deltaTime;

        mainMenu.ChangeLostedTime(lastedtime);
    }

    public void OnPlayerDeath()
    {
        //I know this is a shit
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        cameraSwitherHandler.SetActive(false);

        loseMenu.Show(180 - lastedtime);
    }

    public void ShowLookPanel()
    {
        loseMenu.Disable();
        lookMenu.SetActive(true);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }


}
