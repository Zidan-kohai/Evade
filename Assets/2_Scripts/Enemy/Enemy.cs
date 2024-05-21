using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Burst.Intrinsics.X86;

public class Enemy : MonoBehaviour, IEnemy, ISee, IHumanoid
{
    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private int currentPatrolPositionIndex;
    [SerializeField] private bool sawPlayer;
    [SerializeField] private EnemyState state;
    [SerializeField] private List<Transform> patrolTransform;

    [Header("Field of View")]
    [SerializeField] private ReachArea reachArea;
    [SerializeField] private Vector3 biasOnThrowRaycast;
    [SerializeField] private float fieldOfViewAngle = 90f;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private LayerMask allLayers;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer enemyVisual;
    [SerializeField] private AudioSource audiosource;

    [Header("Idle")]
    [SerializeField] private float lastedTimeFromStartIdleToRotate = 0f;
    [SerializeField] private float timeToChangeState = 5f;
    [SerializeField] private float currentTimeToChangeState = 0f;
    [SerializeField] private int inverseRotateOnIdle = 1;
    [SerializeField] private int rotatingSpeedOnIdle = 5;
    [SerializeField] private int chanseToChangeState = 5;

    [Space]
    [Header("Chase")]
    [SerializeField] private List<IPlayer> playersOnReachArea = new List<IPlayer>();
    [SerializeField] private Dictionary<IPlayer, Vector3> lastSeenPlayersWithPosition = new Dictionary<IPlayer, Vector3>();
    [SerializeField] private float distanseToAttack = 1.5f;

    [Header("Voise")]
    [SerializeField] private float minVoiseTime;
    [SerializeField] private float maxVoiseTime;

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                OnIdle();
                break;
            case EnemyState.Patrol:
                OnPatrol();
                break;
            case EnemyState.Chase:
                OnChase();
                break;
        }

        CheckPlayers();
    }

    public void Initialize(EnemyData data, List<Transform> PatrolPoint, Vector3 spawnPoint)
    {
        ChangeState(EnemyState.Idle);

        transform.position = spawnPoint;
        enemyVisual.sprite = data.enemyVisual;
        audiosource.clip = data.voise;
        gameObject.SetActive(true);
        reachArea.SetISee(this);
        agent.speed = speed;

        patrolTransform = PatrolPoint;
        currentPatrolPositionIndex = UnityEngine.Random.Range(0, patrolTransform.Count);

        PlaySound();
    }

    public void IncreaseSoundZone()
    {
        audiosource.maxDistance *= 2; 
    }

    public void EnableAgent()
    {
        agent.isStopped = true;
        agent.enabled = false;
    }

    public void DisableAgent()
    {
        agent.isStopped = false;
        agent.enabled = true;
    }

    public void AddHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IPlayer player))
        {
            playersOnReachArea.Add(player);
        }
    }

    public void RemoveHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IPlayer player))
        {
            playersOnReachArea.Remove(player);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void PlaySound()
    {
        audiosource.Play();

        StartCoroutine(Wait(minVoiseTime, maxVoiseTime, PlaySound));
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
            ChangeState(EnemyState.Patrol);
            currentTimeToChangeState = 0f;
        }
    }

    private void OnPatrol()
    {
        agent.SetDestination(patrolTransform[currentPatrolPositionIndex].position);
        StartCoroutine(Wait(0.1f, () =>
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPatrolPositionIndex = (currentPatrolPositionIndex + 1) % patrolTransform.Count;
            }
        }));
    }

    private void OnChase()
    {
        float distanse = float.PositiveInfinity;
        Vector3 targetPosition = transform.position;
        IPlayer currentChasingPlayer = null;

        for (int i = 0; i < lastSeenPlayersWithPosition.Count; i++)
        {
            IPlayer player = lastSeenPlayersWithPosition.ElementAt(i).Key;
            Vector3 position = lastSeenPlayersWithPosition.ElementAt(i).Value;

            if (player.IsFallOrDeath())
            {
                lastSeenPlayersWithPosition.Remove(player);
                playersOnReachArea.Remove(player);
                continue;
            }

            float distanceFlag = (player.GetTransform().position - transform.position).magnitude;
            if (distanceFlag < distanse)
            {
                distanse = distanceFlag;
                targetPosition = position;
                currentChasingPlayer = player;
            }
        }

        if (currentChasingPlayer != null)
        {
            agent.SetDestination(targetPosition);

            bool isAttaked = TryAttack(currentChasingPlayer);

            if (isAttaked)
            {
                lastSeenPlayersWithPosition.Remove(currentChasingPlayer);
                return;
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance && currentChasingPlayer != null)
        {
            lastSeenPlayersWithPosition.Remove(currentChasingPlayer);
        }

        if (lastSeenPlayersWithPosition.Count == 0)
        {
            ChangeState(EnemyState.Idle);
        }
    }

    private bool TryAttack(IPlayer currentChasingPlayer)
    {
        float distanceToPlayer = (currentChasingPlayer.GetTransform().position - transform.position).magnitude;

        if (distanceToPlayer < distanseToAttack)
        {
            currentChasingPlayer.Fall();

            playersOnReachArea.Remove(currentChasingPlayer);

            return true;
        }

        return false;
    }

    private void CheckPlayers()
    {
        for (int i = 0; i < playersOnReachArea.Count; i++)
        {
            RaycastHit hit;
            Vector3 direcrtion = playersOnReachArea[i].GetTransform().position - transform.position;

            if (Physics.Raycast(transform.position + biasOnThrowRaycast, (direcrtion + biasOnThrowRaycast).normalized, out hit, Mathf.Infinity, allLayers, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.TryGetComponent(out IPlayer IPlayer) && !IPlayer.IsFallOrDeath())
                {
                    ChangeState(EnemyState.Chase);

                    if (!lastSeenPlayersWithPosition.ContainsKey(IPlayer))
                    {
                        lastSeenPlayersWithPosition.Add(IPlayer, IPlayer.GetTransform().position);
                    }
                    else
                    {
                        lastSeenPlayersWithPosition[IPlayer] = hit.transform.position;
                    }
                }
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (state == newState) return;

        state = newState;

        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Chase:
                break;
            default:
                break;
        }

    }
    
    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }

    private IEnumerator Wait(float minVoiseTime, float maxVoiseTime, Action action)
    {
        float RandomTime = UnityEngine.Random.Range(minVoiseTime, maxVoiseTime);

        yield return new WaitForSeconds(RandomTime);

        action?.Invoke();
    }

    private void OnDrawGizmos()
    {
        // Drawing Field of View
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * viewDistance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);

        Gizmos.color = Color.black;

        for (int i = 0; i < playersOnReachArea.Count; i++)
        {
            Vector3 direcrtion = playersOnReachArea[i].GetTransform().position - transform.position;
            Gizmos.DrawRay(transform.position + biasOnThrowRaycast, direcrtion + biasOnThrowRaycast);
        }

    }

    //Maybe i change this solution in future
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.TryGetComponent(out IPlayer player))
        {
            Debug.Log("collision.transform.name: " + col.transform.name);
            player.Fall();

            if (lastSeenPlayersWithPosition.ContainsKey(player))
            {
                lastSeenPlayersWithPosition.Remove(player);
            }
        }
    }
}
