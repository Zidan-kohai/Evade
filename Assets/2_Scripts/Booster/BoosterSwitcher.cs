using GeekplaySchool;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterSwitcher : MonoBehaviour
{
    [SerializeField] private List<Cell> cells;
    [SerializeField] private List<ShopItemData> Boosters;
    [SerializeField] private Sprite defaultSprite;

    //Need refactoring
    public void Initialize(ShopItem currentHandItem, TextMeshProUGUI equipButtonTextView)
    {
        gameObject.SetActive(true);

        foreach (Cell cell in cells) 
        {
            cell.buttonSelf.onClick.RemoveAllListeners();

            cell.buttonSelf.onClick.AddListener(() =>
            {
                equipButtonTextView.text = "Экипировано";

                Debug.Log("cell");
                int cellIndex = cell.index;
                Geekplay.Instance.PlayerData.SetCurrentBoosterKey(currentHandItem.GetIndexOnPlayer, ref cellIndex);
                cell.cellIcon.sprite = currentHandItem.GetMainIcn;
                gameObject.SetActive(false);

                if(cellIndex != -1)
                {
                    cells[cellIndex].cellIcon.sprite = defaultSprite;
                    Debug.Log("sprite: " + cells[cellIndex].cellIcon.sprite);
                }   
            });
        }

        //Need refactoring
        if (Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID == 7)
        {
            for (int i = 0; i < Geekplay.Instance.PlayerData.CurrentBoosterKeys.Count; i++)
            {
                cells[i].mainObject.SetActive(true);

                for (int j = i; j < cells.Count; j++)
                {
                    bool result = false;
                    foreach (ShopItemData booster in Boosters)
                    {
                        if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == booster.indexOnPlayer)
                        {
                            cells[j].cellIcon.sprite = booster.mainIcon;
                            result = true;
                            break;
                        }
                    }
                    if (result) break;
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                cells[i].mainObject.SetActive(true);

                for (int j = i; j < cells.Count; j++)
                {
                    bool result = false;
                    foreach (ShopItemData booster in Boosters)
                    {
                        if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == booster.indexOnPlayer)
                        {
                            cells[j].cellIcon.sprite = booster.mainIcon;
                            result = true;
                            break;
                        }
                    }
                    if (result) break;
                }
            }
        }
    }
}

[Serializable]
public class Cell
{
    public GameObject mainObject;
    public Image cellIcon;
    public Button buttonSelf;
    public int index;
}