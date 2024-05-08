using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Space]
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private EnemyDataHandler enemyDataHandler;
    private void Awake()
    {
        SpawnUnits();
    }

    protected override void SpawnUnits()
    {
        int enemyCountToSpawn = Random.Range(minSpawnCount, enemies.Count);

        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            Vector3 spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
            EnemyData data = enemyDataHandler.GetRandomEnemyData();
            enemies[i].Initialize(data, PatrollPoints, spawnPoint);
        }

        spawnedEnemyCount = enemyCountToSpawn;
    }

    protected override void SpawnUnit()
    {

    }
}
