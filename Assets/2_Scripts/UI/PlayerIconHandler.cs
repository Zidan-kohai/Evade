using System.Collections.Generic;
using UnityEngine;

public class PlayerIconHandler : MonoBehaviour
{
    public RectTransform Handler;
    public int MapId;

    public List<int> playersIndex;

    public void AddPlayer(int index)
    {
        playersIndex.Add(index);
    }

    public void RemovePlayer(int index)
    {
        playersIndex.Remove(index);
    }
}