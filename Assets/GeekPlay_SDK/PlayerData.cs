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

    public List<int> buyedSkineID = new List<int>();

    public List<int> Codes = new List<int>();
}