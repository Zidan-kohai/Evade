using System.Collections.Generic;
using UnityEngine;

public class PlayerStateShower : MonoBehaviour
{
    private static PlayerStateShower instance;

    [SerializeField] private List<StateShower> itemHandler;


    public void Start()
    {
        instance = this;
    }


    public static void ShowAIState(string name, PlayerState state)
    {
        instance.ShowAI(name, state);
    }

    public static void ShowState(PlayerState state)
    {
        instance.Show(state);
    }

    private void ShowAI(string name, PlayerState state)
    {
        foreach(var item in itemHandler)
        {
            if(!item.isUded)
            {
                item.ShowAI(name, state, this);
                return;
            }
        }
    }

    private void Show(PlayerState state)
    {
        foreach (var item in itemHandler)
        {
            if (!item.isUded)
            {
                item.Show(state, this);
                return;
            }
        }
    }
}
