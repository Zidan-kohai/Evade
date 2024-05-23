using GeekplaySchool;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Daily Exercise/Exercise Data")]
public class DailyExerciseData : ScriptableObject
{
    [SerializeField] private string nameRu;
    [SerializeField] private string nameEn;
    [SerializeField] private string nameTr;

    [SerializeField] private string descriptionRu;
    [SerializeField] private string descriptionEn;
    [SerializeField] private string descriptionTr;


    public string GetName()
    {
        if(Geekplay.Instance.language == "en")
        {
            return nameEn;
        }
        else if(Geekplay.Instance.language == "tr")
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
}
