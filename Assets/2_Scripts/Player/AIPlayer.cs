using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : MonoBehaviour, IPlayer, ISee, IHumanoid
{
    [Header("Transform")]
    [SerializeField] private float startSpeedOnPlayerUp = 1f;
    [SerializeField] private float maxSpeedOnPlayerUp = 5f;
    [SerializeField] private float startSpeedOnPlayerFall = 0.5f;
    [SerializeField] private float maxSpeedOnPlayerFall = 1f;
    [SerializeField] private float currrentSpeed = 1f;

    [Space, Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private PlayerState state;

    [Header("Enemy")]
    [SerializeField] List<IEnemy> enemies;
    [SerializeField] ReachArea reachArea;

    public void Initialize()
    {
        agent.speed = startSpeedOnPlayerUp;
        reachArea.SetISee(this);
    }

    public void AddHumanoid(IHumanoid IHumanoid)
    {
        if(IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void RemoveHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void Fall()
    {
        ChangeState(PlayerState.Fall);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsFallOrDeath()
    {
        return state == PlayerState.Fall || state == PlayerState.Death;
    }


    private void ChangeState(PlayerState newState)
    {
        if (state == newState ||
            ((state == PlayerState.Fall) &&
            (newState != PlayerState.Raising || newState != PlayerState.Death))) return;

        state = newState;

        switch (state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Fall:
                Debug.Log("Fall");
                break;
            case PlayerState.Death:
                break;
        }
    }
}
