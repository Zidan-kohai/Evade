using UnityEngine;

public interface IPlayer
{
    public bool IsFallOrDeath();

    public Transform GetTransform();

    public void Fall();
}
