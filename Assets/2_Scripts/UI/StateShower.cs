using GeekplaySchool;
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
                if (Geekplay.Instance.language == "ru")
                {
                    text.text = $"<color=yellow> {name}, упал</color>";
                }
                else if(Geekplay.Instance.language == "en")
                {
                    text.text = $"<color=yellow> {name}, Fall</color>";
                }
                else
                {
                    text.text = $"<color=yellow> {name}, Dusmek</color>";
                }
                break;
            case PlayerState.Death:
                if (Geekplay.Instance.language == "ru")
                {
                    text.text = $"<color=red> {name}, умер</color>";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    text.text = $"<color=yellow> {name}, Die</color>";
                }
                else
                {
                    text.text = $"<color=yellow> {name}, Olmek</color>";
                }
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
                if (Geekplay.Instance.language == "ru")
                {
                    text.text = $"<color=yellow> вы, упали</color>";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    text.text = $"<color=yellow> You, fall</color>";
                }
                else
                {
                    text.text = $"<color=yellow> Sen, Dusmek</color>";
                }
                break;
            case PlayerState.Death:
                if (Geekplay.Instance.language == "ru")
                {
                    text.text = $"<color=red> Вы, умерли</color>";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    text.text = $"<color=red> You, die</color>";
                }
                else
                {
                    text.text = $"<color=red> Sen, Olmek</color>";
                }
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
