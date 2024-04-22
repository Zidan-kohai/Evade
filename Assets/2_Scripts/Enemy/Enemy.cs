using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private float speed = 5;
    [SerializeField] private List<Transform> patrolTransform;
    [SerializeField] private EnemyState state;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer enemyVisual;
    [SerializeField] private AudioSource audiosource;

    public void Initialize(EnemyData data)
    {
        transform.position = patrolTransform[Random.Range(0, patrolTransform.Count)].position;
        enemyVisual.sprite = data.enemyVisual;
        audiosource.clip = data.voise;

        gameObject.SetActive(true);
    }
}

public enum EnemyState
{
    Idle,
    Patrol,
    Chase
}