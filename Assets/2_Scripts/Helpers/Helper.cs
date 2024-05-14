using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static string SurviveLeaderboardName = "Survive";
    public static string HelpLeaderboardName = "Help";
    public static string DonatLeaderboardName = "Donat";


    public static List<string> ruPlayerName = new List<string>()
    {   
            "Anton",
            "Max",
            "Misha",
            "Tolya",
            "Vlad",
            "Andrey",
            "Bear",
            "Alexei",
    };

    public static List<string> enPlayerName = new List<string>()
    {
            "Anton",
            "Max",
            "Misha",
            "Tolya",
            "Vlad",
            "Andrey",
            "Bear",
            "Alexei",
    };
    public static List<string> trPlayerName = new List<string>()
    {
            "Anton",
            "Maks",
            "Misha",
            "Tolya",
            "Vlad",
            "Andrey",
            "Ayi",
            "Alexei",
    };

    public static string GetRandomName()
    {
        return ruPlayerName[Random.Range(0, ruPlayerName.Count - 1)];

        if (Geekplay.Instance.language == "ru")
        {
            return ruPlayerName[Random.Range(0, ruPlayerName.Count - 1)];
        }
        else if (Geekplay.Instance.language == "en")
        {
            return enPlayerName[Random.Range(0, enPlayerName.Count - 1)];
        }
        else
        {
            return trPlayerName[Random.Range(0, trPlayerName.Count - 1)];
        }
    }
}