using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public Sprite enemyVisual;
    public AudioClip voise;
}
