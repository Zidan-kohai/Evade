using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int Gold = 10;
    public string LastBuy;
    public bool Review;
    public bool IsVolumeOn = true;
    public bool IsGeometryDashRewardTaked;
    public bool IsCloesChangeRewardTaked;
    public bool IsSlapBattleRewardTaked;
    public bool IsTwoPlayerGameRewardTaked;

    public List<MyDictinary> BuyedAccessoryID = new List<MyDictinary>();
    public List<MyDictinary> BuyedItemID = new List<MyDictinary>();
    public List<MyDictinary> BuyedLightID = new List<MyDictinary>();
    public List<MyDictinary> BuyedBoosterID = new List<MyDictinary>();


    public List<int> Codes = new List<int>();
}

[Serializable]
public class MyDictinary
{
    public int key;
    public int value;
}