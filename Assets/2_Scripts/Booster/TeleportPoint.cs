using System;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public event Action<TeleportPoint, IPlayer> enterAction;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            if (player.GetIsTeleport()) return;

            enterAction?.Invoke(this, player);
        }
    }
}
