
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Shop/Item Data")]
public class ShopItemData : ScriptableObject
{
    public SubjectType type;
    public int indexOnPlayer;
    public string name;
    public string description;
    public int cost;
    public bool oneTimePurchase;
}
