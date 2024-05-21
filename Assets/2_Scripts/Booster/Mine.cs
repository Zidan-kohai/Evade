using UnityEngine;

public class Mine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEnemy enemy))
        {
            enemy.DisableAgent();
            Destroy(gameObject);
        }
        else if(other.TryGetComponent(out IPlayer player))
        {
            if (player.IsFallOrDeath()) return;

            player.Fall();
            Destroy(gameObject);
        }
        else if(other.TryGetComponent(out IRealyPlayer realyPlayer))
        {
            if (realyPlayer.IsFallOrDeath()) return;

            realyPlayer.Fall();
            Destroy(gameObject.transform.parent);
        }

    }
}
