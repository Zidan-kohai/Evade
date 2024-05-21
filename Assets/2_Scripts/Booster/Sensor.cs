using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IEnemy enemy))
        {
            audio.Play();
        }
    }
}
