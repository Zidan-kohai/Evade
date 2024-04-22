using UnityEngine;

public class EnemyReachArea : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out IPlayer IPlayer))
        {
            enemy.AddIPlayer(IPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out IPlayer IPlayer))
        {
            enemy.RemoveIPlayer(IPlayer);
        }
    }
}
