using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner
{
    [SerializeField] private List<AIPlayer> players;
    [SerializeField] private List<Transform> Points;
    [SerializeField] private int minSpawnCount;
    [SerializeField] private int spawnedEnemyCount;
    private void Awake()
    {
        SpawnUnits();
    }

    protected override void SpawnUnits()
    {
        int enemyCountToSpawn = Random.Range(minSpawnCount, players.Count);

        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            players[i].Initialize(Points, Points[Random.Range(0, Points.Count)].transform.position);
        }

        spawnedEnemyCount = enemyCountToSpawn;
    }

    protected override void SpawnUnit()
    {

    }
}
