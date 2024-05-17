using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterSwitcher : MonoBehaviour
{
    [SerializeField] private List<Cell> cells;
    [SerializeField] private List<ShopItemData> Boosters;

    public void Start()
    {
        // need to write start initialize 
    }
}

[Serializable]
public class Cell
{
    public Image cell;
    public int index;
}