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

    public event Action BoosterChange;

    //Need refactoring
    public void Initialize(ShopItem currentHandItem, TextMeshProUGUI equipButtonTextView)
    {
        gameObject.SetActive(true);

        foreach (Cell cell in cells) 
        {
            cell.buttonSelf.onClick.RemoveAllListeners();

            cell.buttonSelf.onClick.AddListener(() =>
            {
                if (Geekplay.Instance.language == "ru")
                {
                    equipButtonTextView.text = "Ёкипировано";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    equipButtonTextView.text = "Equipped";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    equipButtonTextView.text = "Donanimli";
                }

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
                currentHandItem.Equip();

                BoosterChange?.Invoke();
            });
        }

        int count = 3;

        if (Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID == 7)
            count = 5;

        for (int i = 0; i < count; i++)
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

[Serializable]
public class Cell
{
    public GameObject mainObject;
    public Image cellIcon;
    public Button buttonSelf;
    public int index;
}