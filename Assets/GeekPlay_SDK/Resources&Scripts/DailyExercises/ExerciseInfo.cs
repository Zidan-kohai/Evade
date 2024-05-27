using GeekplaySchool;
using System;

[Serializable]
public class ExerciseInfo
{
    public string DescriptionRu;
    public string DescriptionEn;
    public string DescriptionTr;

    public int MaxProgress;

    public ExerciseRewardType RewardType;
    public int RewardCount;

    public string GetDescription()
    {
        if(Geekplay.Instance.language == "en")
        {
            return DescriptionEn;
        }
        else if (Geekplay.Instance.language == "tr")
        {
            return DescriptionTr;
        }

        return DescriptionRu;
    }
}
