using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Accessory
{
    public GameObject gameObject;
    public int index;
    public UnityEvent Event;
}