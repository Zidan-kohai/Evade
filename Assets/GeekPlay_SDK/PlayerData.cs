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
    public bool IsGeometryDashRewardTaked;
    public bool IsCloesChangeRewardTaked;
    public bool IsSlapBattleRewardTaked;
    public bool IsTwoPlayerGameRewardTaked;

    public List<int> BuyedAccessoryID = new List<int>() { 0 };
    public int CurrentEquipedAccessoryID = 0;
    public List<int> BuyedLightID = new List<int>(); 
    public int CurrentEquipedLightID = 0;
    public List<MyDictinary> BuyedItemID = new List<MyDictinary>();
    public List<MyDictinary> BuyedBoosterID = new List<MyDictinary>();


    public List<int> Codes = new List<int>();
}

[Serializable]
public class MyDictinary
{
    public int key;
    public int value;
}