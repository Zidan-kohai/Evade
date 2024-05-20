using GeekplaySchool;
using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class BoosterSwitcher : MonoBehaviour
{
    [SerializeField] private List<Cell> cells;
    [SerializeField] private List<ShopItemData> Boosters;

    //Need refactoring
    public void Initialize(ShopItem currentHandItem)
    {
        gameObject.SetActive(true);

        foreach (Cell cell in cells) 
        {
            cell.buttonSelf.onClick.AddListener(() =>
            {
                Geekplay.Instance.PlayerData.SetCurrentBoosterKey(currentHandItem.GetIndexOnPlayer, cell.index);
                cell.cellIcon.sprite = currentHandItem.GetMainIcn;
                gameObject.SetActive(false);
            });
        }


        //need fix logic bag
        for(int i = 0; i < Geekplay.Instance.PlayerData.CurrentBoosterKeys.Count; i++)
        {
            for (int j = i; j < cells.Count; j++)
            {
                foreach (ShopItemData booster in Boosters)
                {
                    if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == booster.indexOnPlayer)
                    {
                        cells[j].cellIcon.sprite = booster.mainIcon;
                    }
                }
            }
        }
    }
}

[Serializable]
public class Cell
{
    public Image cellIcon;
    public Button buttonSelf;
    public int index;
}