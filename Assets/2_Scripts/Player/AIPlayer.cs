using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : MonoBehaviour, IPlayer, ISee, IHumanoid
{
    [Header("Movement")]
    [SerializeField] private List<Transform> pointsToWalk;
    [SerializeField] private int currnetWalkPointIndex;
    [SerializeField] private float startSpeedOnPlayerUp = 1f;
    [SerializeField] private float maxSpeedOnPlayerUp = 5f;
    [SerializeField] private float startSpeedOnPlayerFall = 0.5f;
    [SerializeField] private float maxSpeedOnPlayerFall = 1f;
    [SerializeField] private float currrentSpeed = 1f;

    [Space, Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ReachArea reachArea;

    [Header("Humanoids")]
    [SerializeField] private List<IEnemy> enemies = new List<IEnemy>();
    [SerializeField] private List<IPlayer> players = new List<IPlayer>();

    [Header("State")]
    [SerializeField] private PlayerState state;
    [SerializeField] private float timeToUpFromFall;
    [SerializeField] private float timeToDeathFromFall;
    [SerializeField] private float passedTimeFromFallToUp;
    [SerializeField] private float lastedTimeFromFallToDeath;


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

    [Header("Visual On Change State"), Tooltip("In Future We need To Delete this all")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color colorOnUpState;
    [SerializeField] private Color colorOnFallState;
    [SerializeField] private Color colorOnDeathState;


    //Later we delete this func
    private void OnEnable()
    {
        agent.speed = startSpeedOnPlayerUp;
        reachArea.SetISee(this);
    }

    private void Update()
    {
        switch(state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Walk:
                Walk();
                break;
            case PlayerState.Escape:
                MoveAwayFromEnemies();
                break;
            case PlayerState.Fall:
                break;
            case PlayerState.Death:
                break;
            case PlayerState.Raising:
                break;
        }
    }

    public void Initialize()
    {
        agent.speed = startSpeedOnPlayerUp;
        reachArea.SetISee(this);
    }

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
        passedTimeFromFallToUp -= Time.deltaTime;

        if(passedTimeFromFallToUp <= 0)
        {
            ChangeState(PlayerState.Idle);
        }

        return Mathf.Abs(passedTimeFromFallToUp / timeToUpFromFall - 1);
    }

    public float GetPercentOfRaising()
    {
        return Mathf.Abs(passedTimeFromFallToUp / timeToUpFromFall - 1);
    }

    private void ChangeState(PlayerState newState)
    {
        if (state == newState ||
            ((state == PlayerState.Fall) && (passedTimeFromFallToUp > 0))) return;

        state = newState;

        switch (state)
        {
            case PlayerState.Idle:
                ChangeColor(colorOnUpState);
                Debug.Log(gameObject.name + "Idle");
                break;
            case PlayerState.Walk:
                break;
            case PlayerState.Fall:
                ChangeColor(colorOnFallState);
                Debug.Log(gameObject.name + "Fall");
                passedTimeFromFallToUp = timeToUpFromFall; 
                break;
            case PlayerState.Death:
                ChangeColor(colorOnDeathState);
                Debug.Log(gameObject.name + "Death");
                break;
        }
    }

    private void ChangeColor(Color color)
    {
        meshRenderer.materials[0].color = color;
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
            ChangeState(PlayerState.Walk);
            currentTimeToChangeState = 0f;
        }
    }

    private void Walk()
    {
        agent.SetDestination(pointsToWalk[currnetWalkPointIndex].position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currnetWalkPointIndex = (currnetWalkPointIndex + 1) % pointsToWalk.Count;
        }
    }

    private void MoveAwayFromEnemies()
    {
        Vector3 escapeDirection = CalculateEscapeDirection();
        Vector3 escapePoint = transform.position + escapeDirection * agent.speed;

        if (CheckGround(escapePoint))
        {
            agent.SetDestination(escapePoint);
        }
        else
        {
            // Try different directions if the direct escape route is not viable
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
        // Check alternative directions, here simplified to right and left
        Vector3[] alternatives = { transform.right, -transform.right };
        foreach (var direction in alternatives)
        {
            Vector3 point = transform.position + direction * agent.speed;
            if (CheckGround(point))
            {
                agent.SetDestination(point);
                return;
            }
        }

        // If no valid ground found, stop or handle accordingly
        Debug.Log("No valid escape route found!");
        agent.SetDestination(transform.position); // Stay in place
    }

    private IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }
}
