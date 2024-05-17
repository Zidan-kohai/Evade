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
        int playerCountToSpawn = Random.Range(minSpawnCount, players.Count);

        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
            players[i].Initialize(PatrollPoints, spawnPoint);
        }

        spawnedEnemyCount = playerCountToSpawn;
    }

    protected override void SpawnUnit()
    {

    }
}
