using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class Bait : MonoBehaviour, IPlayer, IHumanoid
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int WaitTimeToDestroy = 30;
    [SerializeField] private int speed;


    private void Start()
    {
        StartCoroutine(Wait(() => Destroy(gameObject), WaitTimeToDestroy));

        rb.velocity = transform.forward * speed;
    }

    private IEnumerator Wait(Action action, int waitTimeToDestroy)
    {
        yield return new WaitForSeconds(waitTimeToDestroy);

        action?.Invoke();
    }

    public void Carried(Transform point, CinemachineVirtualCamera virtualCamera)
    {
        return;
    }

    public void Fall()
    {
        Destroy(gameObject);
    }

    public void GetDownOnGround()
    {
        return;
    }

    public int GetEarnedExperrience()   
    {
        return 0;
    }

    public int GetEarnedMoney()
    {
        return 0;
    }

    public int GetHelpCount()
    {
        return 0; 
    }

    public bool GetIsTeleport()
    {
        return true;
    }

    public string GetName()
    {
        return "";
    }

    public float GetPercentOfRaising()
    {
        return 0;
    }

    public float GetSurvivedTime()
    {
        return 0;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsDeath()
    {
        return false;
    }

    public bool IsFall()
    {
        return false;
    }

    public bool IsFallOrDeath()
    {
        return false;
    }

    public float Raising()
    {
        return 0;
    }

    public void SetTimeToUp(int deacreaseFactor)
    {
    }

    public void SubscribeOnFall(Action<IPlayer> onPlayerDeath)
    {
    }

    public void Teleport(Vector3 teleportPosition)
    {
    }
}
