using Cinemachine;
using UnityEngine;

public interface IPlayer
{
    public bool IsFall();

    public bool IsDeath();

    public bool IsFallOrDeath();

    public Transform GetTransform();

    public void Fall();

    public float Raising();

    public float GetPercentOfRaising();

    public string GetName();

    public int GetEarnedMoney();

    public int GetEarnedExperrience();

    public int GetHelpCount();

    public float GetSurvivedTime();

    public void SetTimeToUp(int deacreaseFactor);

    public void Carried(Transform point, CinemachineVirtualCamera virtualCamera);

    public void GetDownOnGround();
}

public interface IRealyPlayer
{
    public void SetMoneyMulltiplierFactor(int value);

    public void SetExperienceMulltiplierFactor(int value);

    public void SetMaxSpeedOnFall(int increaseFactor);

    public void SetMaxSpeedOnUp(int increaseFacctor);
}