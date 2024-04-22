using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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

    [Header("Idle")]
    [SerializeField] private float lastedTimeFromStartIdleToRotate = 0f;
    [SerializeField] private float timeToChangeState = 5f;
    [SerializeField] private float currentTimeToChangeState = 0f;
    [SerializeField] private int inverseRotateOnIdle = 1;
    [SerializeField] private int rotatingSpeedOnIdle = 5;
    [SerializeField] private int chanseToChangeState = 5;
    public void Initialize(EnemyData data)
    {
        transform.position = patrolTransform[UnityEngine.Random.Range(0, patrolTransform.Count)].position;
        enemyVisual.sprite = data.enemyVisual;
        audiosource.clip = data.voise;
        currentPatrolPositionIndex = UnityEngine.Random.Range(0, patrolTransform.Count);
        gameObject.SetActive(true);

        state = EnemyState.Idle;
    }

    private void Update()
    {
        switch(state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
        }
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

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolPositionIndex = (currentPatrolPositionIndex + 1) % patrolTransform.Count;
        }

    }

    private void Chase()
    {

    }


    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }
}