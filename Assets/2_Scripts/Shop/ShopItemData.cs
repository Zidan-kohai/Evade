
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Shop/Item Data")]
public class ShopItemData : ScriptableObject
{
    public SubjectType type;
    public string name;
    public string description;
    public string cost;
    public bool oneTimePurchase;
}
