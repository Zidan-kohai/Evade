using UnityEngine;

public interface IEnemy
{
    public Transform GetTransform();
    public void EnableAgent();
    public void DisableAgent();
}