
using GeekplaySchool;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Shop/Item Data")]
public class ShopItemData : ScriptableObject
{
    public SubjectType type;
    public Sprite mainIcon;
    public Sprite secondIcon;
    public int openOnLevel;
    public int indexOnPlayer;
    public string nameRu;
    public string nameEn;
    public string nameTr;
    public string descriptionRu;
    public string descriptionEn;
    public string descriptionTr;
    public int cost;
    public bool oneTimePurchase;

    public string Name()
    {
        if (Geekplay.Instance.language == "en")
        {
            return nameEn;
        }
        else if (Geekplay.Instance.language == "tr")
        {
            return nameTr;
        }

        return nameRu;
    }

    public string Description()
    {
        if (Geekplay.Instance.language == "en")
        {
            return descriptionEn;
        }
        else if (Geekplay.Instance.language == "tr")
        {
            return descriptionTr;
        }

        return descriptionRu;
    }
}
