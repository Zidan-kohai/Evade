
using GeekplaySchool;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Item Data", menuName = "Shop/Item Data")]
public class ShopItemData : ScriptableObject
{
    public SubjectType type;
    public Sprite mainIcon;
    public Vector2 mainIconSize = new Vector2(200, 65);
    public Sprite secondIcon;
    public Vector2 secondIconSize = new Vector2(100, 100);
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
