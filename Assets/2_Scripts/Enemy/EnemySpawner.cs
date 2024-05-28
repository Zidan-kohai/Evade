using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Space]
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private EnemyDataHandler enemyDataHandler;
    [SerializeField] private int spawnCount;
    private void Awake()
    {
        SpawnUnits();
    }

    protected override void SpawnUnits()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
            EnemyData data = enemyDataHandler.GetRandomEnemyData();
            enemies[i].Initialize(data, PatrollPoints, spawnPoint);
        }

        spawnedEnemyCount = spawnCount;
    }

    protected override void SpawnUnit()
    {

    }
}
