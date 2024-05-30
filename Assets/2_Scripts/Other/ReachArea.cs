using System.Collections.Generic;
using UnityEngine;

public class ReachArea : MonoBehaviour
{
    private List<ISee> see = new List<ISee>();

    public void SetISee(ISee ISee)
    {
        see.Add(ISee);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IHumanoid IHumanoid))
        {
            foreach (ISee ISee in see)
            {
                ISee.AddHumanoid(IHumanoid);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out IHumanoid IHumanoid))
        {
            foreach (ISee ISee in see)
            {
                ISee.RemoveHumanoid(IHumanoid);
            }
        }
    }
}
