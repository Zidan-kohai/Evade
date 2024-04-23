using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy, ISee, IHumanoid
{
    [Header("Transform")]
    [SerializeField] private float speed = 5;
    [SerializeField] private List<Transform> patrolTransform;
    [SerializeField] private int currentPatrolPositionIndex;
    [SerializeField] private EnemyState state;
    [SerializeField] private bool sawPlayer;

    [Header("Field of View")]
    [SerializeField] private ReachArea reachArea;
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

    public void Initialize(EnemyData data)
    {
        transform.position = patrolTransform[UnityEngine.Random.Range(0, patrolTransform.Count)].position;
        enemyVisual.sprite = data.enemyVisual;
        audiosource.clip = data.voise;
        currentPatrolPositionIndex = UnityEngine.Random.Range(0, patrolTransform.Count);
        gameObject.SetActive(true);
        reachArea.SetISee(this);

        state = EnemyState.Idle;
    }

    public void AddHumanoid(IHumanoid IHumanoid)
    {
        if(IHumanoid.gameObject.TryGetComponent(out IPlayer player))
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

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
        }

        CheckPlayers();
    }

    private void Idle()
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
            state = EnemyState.Patrol;
            currentTimeToChangeState = 0f;
        }
    }

    private void Patrol()
    {
        agent.SetDestination(patrolTransform[currentPatrolPositionIndex].position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolPositionIndex = (currentPatrolPositionIndex + 1) % patrolTransform.Count;
        }

    }

    private void Chase()
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

        if(currentChasingPlayer != null)
        {
            agent.SetDestination(targetPosition);

            bool isAttaked = TryAttack(currentChasingPlayer);

            if(isAttaked)
            {
                lastSeenPlayersWithPosition.Remove(currentChasingPlayer);
                return;
            }
        }

        if(agent.remainingDistance <= agent.stoppingDistance && currentChasingPlayer != null)
        {
            lastSeenPlayersWithPosition.Remove(currentChasingPlayer);
        }

        if(lastSeenPlayersWithPosition.Count == 0)
        {
            state = EnemyState.Idle;
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
        for(int i = 0; i < playersOnReachArea.Count; i++)
        {
            RaycastHit hit;
            Vector3 direcrtion = playersOnReachArea[i].GetTransform().position - transform.position;

            if (Physics.Raycast(transform.position, direcrtion.normalized, out hit, Mathf.Infinity, allLayers, QueryTriggerInteraction.Ignore))
            {
                if(hit.transform.TryGetComponent(out IPlayer IPlayer))
                {
                    state = EnemyState.Chase;

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

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }

    private void OnDrawGizmos()
    {
        // Drawing Field of View
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward * viewDistance);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);
    }

}
