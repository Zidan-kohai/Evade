using UnityEngine;

public class ReachArea : MonoBehaviour
{
    private ISee see;

    public void SetISee(ISee ISee)
    {
        see = ISee;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IHumanoid IHumanoid))
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
