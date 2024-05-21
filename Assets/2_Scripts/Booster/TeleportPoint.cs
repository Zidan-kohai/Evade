using System;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public event Action<int, IPlayer> enterAction;
    private int index;
    public void Initizlize(int index)
    {
        this.index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlayer player))
        {
            enterAction?.Invoke(index, player);
        }
    }
}
