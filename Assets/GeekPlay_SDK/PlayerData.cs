using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int Gold = 1000;
    public int Diamond = 100;
    public string LastBuy;
    public bool Review;
    public bool IsVolumeOn = true;
    public int HelpCount;
    public int SurviveCount;
    public int DonatCount;
    public bool IsGeometryDashRewardTaked;
    public bool IsCloesChangeRewardTaked;
    public bool IsSlapBattleRewardTaked;
    public bool IsTwoPlayerGameRewardTaked;

    public List<int> BuyedAccessoryID = new List<int>() { 0 };
    public int CurrentEquipedAccessoryID = 0;
    public List<int> BuyedLightID = new List<int>(); 
    public int CurrentEquipedLightID = 0;
    public List<MyDictionary> BuyedItemID = new List<MyDictionary>();
    public List<MyDictionary> BuyedBoosterID = new List<MyDictionary>();
    

    public List<int> Codes = new List<int>();
}

[Serializable]
public class MyDictionary
{
    public int key;
    public int value;
}