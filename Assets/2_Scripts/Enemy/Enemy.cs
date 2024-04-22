using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private float speed = 5;
    [SerializeField] private List<Transform> patrolTransform;
    [SerializeField] private int currentPatrolPositionIndex;
    [SerializeField] private EnemyState state;
    [SerializeField] private bool sawPlayer;

    [Header("Field of View")]
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

    public void Initialize(EnemyData data)
    {
        transform.position = patrolTransform[UnityEngine.Random.Range(0, patrolTransform.Count)].position;
        enemyVisual.sprite = data.enemyVisual;
        audiosource.clip = data.voise;
        currentPatrolPositionIndex = UnityEngine.Random.Range(0, patrolTransform.Count);
        gameObject.SetActive(true);

        state = EnemyState.Idle;
    }

    public void AddIPlayer(IPlayer IPlayer)
    {
        playersOnReachArea.Add(IPlayer);
    }

    public void RemoveIPlayer(IPlayer IPlayer)
    {
        playersOnReachArea.Remove(IPlayer);
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
        IPlayer currentChasingPlayerForKey = null;

        foreach (var item in lastSeenPlayersWithPosition)
        {
            float distanceFlag = (item.Key.GetTransform().position - transform.position).magnitude;
            if (distanceFlag < distanse)
            {
                distanse = distanceFlag;
                targetPosition = item.Value;
                currentChasingPlayerForKey = item.Key;
            }
        }

        if(currentChasingPlayerForKey != null)
        {
            agent.SetDestination(targetPosition);
        }

        if(agent.remainingDistance <= agent.stoppingDistance && currentChasingPlayerForKey != null)
        {
            lastSeenPlayersWithPosition.Remove(currentChasingPlayerForKey);
        }

        if(lastSeenPlayersWithPosition.Count == 0)
        {
            state = EnemyState.Idle;
        }
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