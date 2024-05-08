using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner
{
    [Space]
    [SerializeField] private List<AIPlayer> players;

    private void Start()
    {
        SpawnUnits();
    }

    protected override void SpawnUnits()
    {
        int enemyCountToSpawn = Random.Range(minSpawnCount, players.Count);

        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            Vector3 spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
            players[i].Initialize(PatrollPoints, spawnPoint);
        }

        spawnedEnemyCount = enemyCountToSpawn;
    }

    protected override void SpawnUnit()
    {

    }
}
