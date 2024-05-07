using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button buyButton;

    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI descriptionView;
    [SerializeField] private TextMeshProUGUI costView;


    [SerializeField] private List<ShopItem> items;
    [SerializeField] private ShopItem firstItem;

    private void Start()
    {
        ShowInfo(firstItem);

        foreach (ShopItem item in items)
        {
            item.SubscribeEvent(() =>
            {
                ShowInfo(item);
                
            });
        }
    }

    private void ShowInfo(ShopItem item)
    {
        nameView.text = item.GetDataName;
        descriptionView.text = item.GetDescription;
        costView.text = item.GetDataCost;
    }
}