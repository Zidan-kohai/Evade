using Cinemachine;
using System;
using UnityEngine;

public interface IPlayer
{
    public bool IsFall();

    public bool IsDeath();

    public bool IsFallOrDeath();

    public Transform GetTransform();

    public bool GetIsTeleport();

    public void Teleport(Vector3 teleportPosition);

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

    public void SubscribeOnFall(Action<IPlayer> onPlayerDeath);
}

public interface IRealyPlayer : IPlayer
{
    public void SetMoneyMulltiplierFactor(int value);

    public void SetExperienceMulltiplierFactor(int value);

    public float SetMaxSpeedOnFall(int value, bool isFactor = true);

    public float SetMaxSpeedOnUp(int value, bool isFactor = true);

}