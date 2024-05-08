using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> SpawnPoints;
    [SerializeField] protected List<Transform> PatrollPoints;
    [SerializeField] protected int minSpawnCount;
    [SerializeField] protected int spawnedEnemyCount;
    protected virtual void SpawnUnits()
    {

    }

    protected virtual void SpawnUnit()
    {

    }
}
