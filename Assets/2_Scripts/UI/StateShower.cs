using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public class StateShower
{
    public GameObject handler;
    public TextMeshProUGUI text;
    public bool isUded;
    public void ShowAI(string name, PlayerState state, MonoBehaviour mono)
    {
        isUded = true;
        handler.SetActive(true);
        switch (state)
        {
            case PlayerState.Fall:
                text.text = $"<color=yellow> {name}, упал</color>";
                break;
            case PlayerState.Death:
                text.text = $"<color=red> {name}, умер</color>";
                break;
        }

        mono.StartCoroutine(Wait(3f, () =>
        {
            isUded = false;
            handler.SetActive(false);
        }));
    }

    public void Show(PlayerState state, MonoBehaviour mono)
    {
        isUded = true;
        handler.SetActive(true);
        switch (state)
        {
            case PlayerState.Fall:
                text.text = $"<color=yellow> вы, упали</color>";
                break;
            case PlayerState.Death:
                text.text = $"<color=red> вы, умерли</color>";
                break;
        }

        mono.StartCoroutine(Wait(3f, () =>
        {
            isUded = false;
            handler.SetActive(false);
        }));
    }
    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
