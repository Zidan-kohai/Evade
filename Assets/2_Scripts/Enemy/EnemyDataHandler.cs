using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataHandler", menuName = "Enemy/Enemy Data Handler")]
public class EnemyDataHandler : ScriptableObject
{
    [SerializeField] private List<EnemyData> enemiesData;    

    public EnemyData GetRandomEnemy()
    {
        return enemiesData[Random.Range(0, enemiesData.Count)];
    }
}
