using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Shop/InApp Shop Item Data")]
public class InAppShopItemData : ScriptableObject
{
    public InAppShopItemType Type;
    public string PurchaseTag;
    public int Goldcount;
    public int DiamondCount;
    public int Cost;
}


public enum InAppShopItemType
{
    Gold,
    Diamond,
    Both
}