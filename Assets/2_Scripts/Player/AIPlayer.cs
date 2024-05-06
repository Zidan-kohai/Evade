using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : MonoBehaviour, IPlayer, ISee, IHumanoid, IMove
{
    [Header("Movement")]
    [SerializeField] private float startSpeedOnPlayerUp = 50f;
    [SerializeField] private float maxSpeedOnPlayerUp = 80f;
    [SerializeField] private float startSpeedOnPlayerFall = 20f;
    [SerializeField] private float maxSpeedOnPlayerFall = 30f;
    [SerializeField] private float currrentSpeed = 50f;
    [SerializeField] private int speedScaleFactor = 3;
    [SerializeField] private float currrentMinSpeed = 1f;
    [SerializeField] private float currrentMaxSpeed = 1f;
    [SerializeField] private List<Transform> pointsToWalk;
    [SerializeField] private int currnetWalkPointIndex;

    [Space, Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ReachArea reachArea;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Humanoids")]
    [SerializeField] private List<IEnemy> enemies = new List<IEnemy>();
    [SerializeField] private List<IPlayer> players = new List<IPlayer>();

    [Header("State")]
    [SerializeField] private PlayerState state;
    [SerializeField] private float timeToUpFromFall;
    [SerializeField] private float timeToDeathFromFall;
    [SerializeField] private float lostedTimeFromFallToUp;
    [SerializeField] private float lostedTimeFromFallToDeath;

    [Header("Idle")]
    [SerializeField] private float lastedTimeFromStartIdleToRotate = 0f;
    [SerializeField] private float timeToChangeState = 5f;
    [SerializeField] private float currentTimeToChangeState = 0f;
    [SerializeField] private int inverseRotateOnIdle = 1;
    [SerializeField] private int rotatingSpeedOnIdle = 5;
    [SerializeField] private int chanseToChangeState = 5;

    [Header("Escape")]
    [SerializeField] private float minEscapeTime;
    [SerializeField] private float maxEscapeTime;
    [SerializeField] private Coroutine stopEscapeCoroutine;

    [Header("Help")]
    [SerializeField] private float safeDistance;
    [SerializeField] private float helpDistance;
    [SerializeField] private int helpCount;
    [SerializeField] private IPlayer playerToHelp;

    [Header("General")]
    [SerializeField] private float livedTime = 0;
    private Coroutine coroutine;
    private string name;

    private void Update()
    {
        if (state == PlayerState.Death) return;

        livedTime += Time.deltaTime;

        switch (state)
        {
            case PlayerState.Idle:
                if(!CheckPlayerAndEnemyToHelp())
                    OnIdle();
                break;
            case PlayerState.Walk:
                if (!CheckPlayerAndEnemyToHelp())
                    OnWalk();
                break;
            case PlayerState.Escape:
                MoveAwayFromEnemies();
                break;
            case PlayerState.Fall:
                OnFall();
                break;
            case PlayerState.Raising:
                break;
            case PlayerState.Death:
                break;
        }

        CheckPlayerAndEnemyToHelp();
    }

    public void Initialize(List<Transform> Points, Vector3 spawnPoint)
    {
        ChangeState(PlayerState.Idle);

        transform.position = spawnPoint;
        agent.speed = startSpeedOnPlayerUp;
        reachArea.SetISee(this);
        animationController.SetIMove(this);
        gameObject.SetActive(true);

        pointsToWalk = Points;

        CameraConrtoller.AddCameraST(this, virtualCamera);

        name = Helper.GetRandomName();

        GameplayController.AddPlayer(this);
    }

    public string GetName() => name;

    public int GetEarnedMoney() => 0;

    public int GetHelpCount() => helpCount;

    public float GetSurvivedTime() => livedTime + 0.2f;

    public void AddHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemies.Add(enemy);
            ChangeState(PlayerState.Escape);
        }
        else if (IHumanoid.gameObject.TryGetComponent(out IPlayer player))
        {
            players.Add(player);
        }
    }

    public void RemoveHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemies.Remove(enemy);

            if(enemies.Count == 0)
            {
                float escapeTime = UnityEngine.Random.Range(minEscapeTime, maxEscapeTime);

                if(stopEscapeCoroutine != null)
                {
                    StopCoroutine(stopEscapeCoroutine);
                }

                stopEscapeCoroutine = StartCoroutine(Wait(escapeTime, () =>
                {
                    if (enemies.Count == 0) ChangeState(PlayerState.Idle);
                }));
            }
        }
        else if (IHumanoid.gameObject.TryGetComponent(out IPlayer player))
        {
            players.Remove(player);
        }
    }

    public void Fall()
    {
        if (state == PlayerState.Death) return;

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

    public bool IsFall()
    {
        return state == PlayerState.Fall;
    }

    public bool IsDeath()
    {
        return state == PlayerState.Death;
    }

    public float Raising()
    {
        ChangeState(PlayerState.Raising);

        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(Wait(0.5f, () =>
        {
            ChangeState(PlayerState.Fall);
        }));

        lostedTimeFromFallToUp -= Time.deltaTime;


        if(lostedTimeFromFallToUp <= 0)
        {
            ChangeState(PlayerState.Idle); 
            StopCoroutine(coroutine);
        }

        return Mathf.Abs(lostedTimeFromFallToUp / timeToUpFromFall - 1);
    }

    public float GetPercentOfRaising()
    {
        return Mathf.Abs(lostedTimeFromFallToUp / timeToUpFromFall - 1);
    }

    public float MoveSpeed()
    {
        return agent.velocity.magnitude;
    }

    public bool IsJump()
    {
        return false;
    }

    private void ChangeState(PlayerState newState)
    {
        if (state == newState ||
            ((state == PlayerState.Fall) && (lostedTimeFromFallToUp > 0) && (newState != PlayerState.Death))) return;

        state = newState;

        switch (state)
        {
            case PlayerState.Idle:
                animationController.Up();
                currrentMinSpeed = startSpeedOnPlayerUp;
                currrentMaxSpeed = maxSpeedOnPlayerUp;
                break;

            case PlayerState.Walk:
                break;

            case PlayerState.Fall:
                PlayerStateShower.ShowAIState(name, state);
                animationController.Fall();
                currrentMinSpeed = startSpeedOnPlayerFall;
                currrentMaxSpeed = maxSpeedOnPlayerFall;
                currrentSpeed = currrentMinSpeed;
                lostedTimeFromFallToUp = timeToUpFromFall;
                lostedTimeFromFallToDeath = timeToDeathFromFall;
                break;

            case PlayerState.Death:
                Death();
                break;
        }
    }

    private void Death()
    {
        if(coroutine != null) StopCoroutine(coroutine);

        PlayerStateShower.ShowAIState(name, state);
        agent.SetDestination(transform.position);
        animationController.Death();
    }

    private void OnIdle()
    {
        lastedTimeFromStartIdleToRotate += Time.deltaTime;
        currentTimeToChangeState += Time.deltaTime;

        if (lastedTimeFromStartIdleToRotate > 3f)
        {
            inverseRotateOnIdle *= -1;
            lastedTimeFromStartIdleToRotate = 0f;
        }

        transform.Rotate(0, rotatingSpeedOnIdle * Time.deltaTime * inverseRotateOnIdle, 0);

        bool endIdle = UnityEngine.Random.Range(0, 1000) < chanseToChangeState;

        if (endIdle || currentTimeToChangeState > timeToChangeState)
        {
            ChangeState(PlayerState.Walk);
            currentTimeToChangeState = 0f;
        }

        currrentSpeed -= Time.deltaTime;
        currrentSpeed = Mathf.Clamp(currrentSpeed, 0, currrentMaxSpeed);
    }

    private void OnWalk()
    {
        SetDestination(pointsToWalk[currnetWalkPointIndex].position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currnetWalkPointIndex = (currnetWalkPointIndex + 1) % pointsToWalk.Count;
            ChangeState(PlayerState.Idle);
        }
    }

    private void OnFall()
    {
        lostedTimeFromFallToDeath -= Time.deltaTime;

        if(lostedTimeFromFallToDeath < 0)
        {
            ChangeState(PlayerState.Death);
        }
        else if(players.Count > 0)
        {
            WalkToPlayerOnFail();
        }
        else
        {
            OnWalk();
        }
    }

    private void WalkToPlayerOnFail()
    {
        bool isHaveUpPlayer = false;
        float distanceToPlayer = float.PositiveInfinity;
        IPlayer nearnestPlayer = null;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsDeath()) continue;

            float distance = (players[i].GetTransform().position - transform.position).magnitude;
            bool isPlayerUp = !players[i].IsFall();

            if (isHaveUpPlayer == false && isPlayerUp == true)
            {
                isHaveUpPlayer = true;
                distanceToPlayer = distance;
                nearnestPlayer = players[i];
            }
            else if (isHaveUpPlayer == isPlayerUp)
            {
                if (distance < distanceToPlayer)
                {
                    distanceToPlayer = distance;
                    nearnestPlayer = players[i];
                }
            }
        }
        if(nearnestPlayer != null) 
            SetDestination(nearnestPlayer.GetTransform().position);
    }

    private void MoveAwayFromEnemies()
    {
        Vector3 escapeDirection = CalculateEscapeDirection();
        Vector3 escapePoint = transform.position + escapeDirection * agent.speed;

        if (CheckGround(escapePoint))
        {
            SetDestination(escapePoint);
        }
        else
        {
            TryAlternativeRoutes(escapeDirection);
        }
    }

    private Vector3 CalculateEscapeDirection()
    {
        Vector3 averageApproachDirection = Vector3.zero;

        foreach (var enemy in enemies)
        {
            Vector3 toEnemy = enemy.GetTransform().position - transform.position;
            averageApproachDirection += toEnemy.normalized * (1.0f / toEnemy.magnitude);
        }
        if (enemies.Count > 0)
        {
            averageApproachDirection /= enemies.Count;
            return -averageApproachDirection.normalized;
        }

        return transform.forward;
    }

    private bool CheckGround(Vector3 point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point + Vector3.up * 1.0f, Vector3.down, out hit, 2.0f))
        {
            return hit.collider != null; // Check if we hit the ground
        }
        return false;
    }

    private void TryAlternativeRoutes(Vector3 initialDirection)
    {
        Vector3[] alternatives = { transform.right, -transform.right };
        foreach (var direction in alternatives)
        {
            Vector3 point = transform.position + direction * agent.speed;
            if (CheckGround(point))
            {
                SetDestination(point);
                return;
            }
        }

        Debug.Log("No valid escape route found!");
        agent.SetDestination(transform.position);
    }

    private bool CheckPlayerAndEnemyToHelp()
    {
        if (state != PlayerState.Idle && state != PlayerState.Walk || players.Count == 0) return false;

        #region CheckPlayer

        float distanceToPlayer = float.PositiveInfinity;
        IPlayer nearnestPlayer = null;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsDeath()) continue;

            float distance = (players[i].GetTransform().position - transform.position).magnitude;
            bool isPlayerFail = players[i].IsFall();

            if(isPlayerFail && distance < distanceToPlayer)
            {
                distanceToPlayer = distance;
                nearnestPlayer = players[i];
            }
        }

        #endregion

        #region Check Enemy

        float distanceToNeanestEnemy = float.PositiveInfinity;
        IEnemy nearnestEnemy = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            float distance = (enemies[i].GetTransform().position - transform.position).magnitude;

            if (distance < distanceToNeanestEnemy)
            {
                distanceToNeanestEnemy = distance;
                nearnestEnemy = enemies[i];
            }
        }

        #endregion

        if (nearnestPlayer == null || distanceToNeanestEnemy < safeDistance) return false;

        playerToHelp = nearnestPlayer;

        TryHelp(nearnestPlayer);

        return true;
    }

    private void TryHelp(IPlayer player)
    {
        float distanceToPlayer = (player.GetTransform().position - transform.position).magnitude;

        if(distanceToPlayer < helpDistance)
        {
            if(player.Raising() >= 1)
            {
                helpCount++;
            }
        }
        else
        {
            SetDestination(player.GetTransform().position);
        }
    }

    private void SetDestination(Vector3 target)
    {
        currrentSpeed += Time.deltaTime * speedScaleFactor;

        currrentSpeed = Mathf.Clamp(currrentSpeed, currrentMinSpeed, currrentMaxSpeed);

        agent.speed = currrentSpeed;
        agent.SetDestination(target);
    }

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }

}
