using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private EnemyDataHandler enemyDataHandler;
    [SerializeField] private int minSpawnCount;
    [SerializeField] private int spawnedEnemyCount;
    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        int enemyCountToSpawn = Random.Range(minSpawnCount, enemies.Count);

        for (int i = 0; i < enemyCountToSpawn; i++)
        {
            enemies[i].Initialize(enemyDataHandler.GetRandomEnemyData());
        }

        spawnedEnemyCount = enemyCountToSpawn;
    }
}
