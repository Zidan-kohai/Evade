using GeekplaySchool;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Map/Item Data")]
public class MapItemData : ScriptableObject
{
    [Header("Name")]
    [SerializeField] private string nameRu;
    [SerializeField] private string nameEn;
    [SerializeField] private string nameTr;

    [Header("Description")]
    [SerializeField] private string descriptionRu;
    [SerializeField] private string descriptionEn;
    [SerializeField] private string descriptionTr;

    [Header("Hardnest")]
    [SerializeField] private string hardnestRu;
    [SerializeField] private string hardnestEn;
    [SerializeField] private string hardnestTr;

    public int SceneIndex;

    public string GetName()
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

    public string GetDescription()
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

    public string GetHardnest()
    {
        if (Geekplay.Instance.language == "en")
        {
            return hardnestEn;
        }
        else if (Geekplay.Instance.language == "tr")
        {
            return hardnestTr;
        }

        return hardnestRu;
    }
}
