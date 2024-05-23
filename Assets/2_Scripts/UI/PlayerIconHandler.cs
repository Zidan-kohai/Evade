using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerIconHandler : MonoBehaviour
{
    [SerializeField] private MapItemData data;
    [SerializeField] private RectTransform Handler;
    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI hardNestView;
    private List<int> playersIndex = new List<int>();

    public int GetMapID => data.SceneIndex;

    public string GetDescription => data.GetDescription();

    public int GetVoiseCount => playersIndex.Count;


    private void Start()
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        nameView.text = data.GetName();
        hardNestView.text = data.GetHardnest();
    }

    public void AddPlayer(RectTransform rect, int index)
    {
        playersIndex.Add(index);
        rect.parent = Handler;
    }

    public void RemovePlayer(int index)
    {
        if (playersIndex.Remove(index))
        {

        }
    }
}