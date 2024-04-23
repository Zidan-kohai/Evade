using UnityEngine;

public class ReachArea : MonoBehaviour
{
    [SerializeField] private ISee see;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out IHumanoid IHumanoid))
        {
            see.AddHumanoid(IHumanoid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out IHumanoid IHumanoid))
        {
            see.RemoveHumanoid(IHumanoid);
        }
    }
}
