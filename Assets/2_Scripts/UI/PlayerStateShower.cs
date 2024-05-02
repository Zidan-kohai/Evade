using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateShower : MonoBehaviour
{
    private static PlayerStateShower instance;

    [SerializeField] private List<StateShower> itemHandler;


    public void Start()
    {
        instance = this;
    }


    public static void ShowState(string name, PlayerState state)
    {
        instance.Show(name, state);
    }

    private void Show(string name, PlayerState state)
    {
        foreach(var item in itemHandler)
        {
            if(!item.isUded)
            {
                item.Show(name, state, this);
                return;
            }
        }
    }
}

[Serializable]
public class StateShower
{
    public GameObject handler;
    public TextMeshProUGUI text;
    public bool isUded;
    public void Show(string name, PlayerState state, MonoBehaviour mono)
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

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
