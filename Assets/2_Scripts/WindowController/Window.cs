using System;
using UnityEngine;

public class Window : MonoBehaviour, IWindow
{
    private static Action<Window> openWindow;

    [SerializeField] private WindowState state = WindowState.Close;

    public void Initialize()
    {
        openWindow += Opening;
    }

    public void Open()
    {
        openWindow?.Invoke(this);
    }

    private void Opening(Window openingWindow)
    {
        if (state == WindowState.Open) 
        {
            Close();
            return;
        }

        
        if (openingWindow == this)
        {
            gameObject.SetActive(true);
            state = WindowState.Open;
            return;
        }

    }

    private void Close()
    {
        gameObject.SetActive(false);
        state = WindowState.Close;
    }
}

public enum WindowState
{
    Open,
    Close
}
