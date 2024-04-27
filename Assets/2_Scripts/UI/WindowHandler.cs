using UnityEngine;
using System.Collections.Generic;
public class WindowHandler : MonoBehaviour
{
    [SerializeField] private List<Window> windows;

    private void Start()
    {
        foreach (var window in windows) 
        {
            window.Initialize();
        } 
    }
}
