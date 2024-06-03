using System.Collections;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private Material material;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    private void Start()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            material.color = color1;
            yield return new WaitForSeconds(0.5f);
            material.color = color2;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IEnemy enemy))
        {
            audio.Play();
        }
    }
}
