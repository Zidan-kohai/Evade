using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button buyButton;

    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI descriptionView;
    [SerializeField] private TextMeshProUGUI costView;


    [SerializeField] private List<ShopItem> items;

    private void Start()
    {
        foreach (ShopItem item in items)
        {
            item.SubscribeEvent(() =>
            {
                nameView.text = item.GetDataName;
                descriptionView.text = item.GetDescription;
                costView.text = item.GetDataCost;
            });
        }
    }
}