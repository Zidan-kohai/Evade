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

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer enemyVisual;
    [SerializeField] private AudioSource audiosource;

    public void Initialize(EnemyData data)
    {
        transform.position = patrolTransform[Random.Range(0, patrolTransform.Count)].position;
        enemyVisual.sprite = data.enemyVisual;
        audiosource.clip = data.voise;
        currentPatrolPositionIndex = Random.Range(0, patrolTransform.Count);
        gameObject.SetActive(true);

        state = EnemyState.Idle;
    }

    private void Update()
    {
        switch(state)
        {
            case EnemyState.Idle:
                state = EnemyState.Patrol;
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        agent.SetDestination(patrolTransform[currentPatrolPositionIndex].position);

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolPositionIndex = (currentPatrolPositionIndex + 1) % patrolTransform.Count;
        }

    }

    private void Chase()
    {

    }
}