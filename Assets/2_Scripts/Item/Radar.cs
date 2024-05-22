using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private AudioSource radarAudio;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;

    private List<Transform> enemies;

    private void Start()
    {
        if (Geekplay.Instance.PlayerData.CurrentEquipedItemID != 3)
            Destroy(gameObject);
    }


    private void Update()
    {
        float nearnestDistanse = float.MaxValue;

        if(enemies.Count == 0)
        {
            radarAudio.volume = 0;
            return;
        }

        foreach (var enemy in enemies)
        {
            float distanse = Vector3.Distance(enemy.position, transform.position);

            if (distanse < nearnestDistanse)
            {
                nearnestDistanse = distanse;
            }
        }

        float radarValue = CalculateValue(minDistance, maxDistance, nearnestDistanse);

        radarAudio.volume = radarValue;
    }

    public float CalculateValue(float minDistance, float maxDistance, float currentDistance)
    {
        // Проверяем граничные условия
        if (currentDistance <= minDistance)
        {
            return 1f;
        }
        if (currentDistance >= maxDistance)
        {
            return 0f;
        }

        // Линейная интерполяция для вычисления значения между 0 и 1
        float value = 1f - ((currentDistance - minDistance) / (maxDistance - minDistance));
        return value;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IEnemy enemy))
        {
            enemies.Add(enemy.GetTransform());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IEnemy enemy) && enemies.Contains(enemy.GetTransform()))
        {
            enemies.Remove(enemy.GetTransform());
        }
    }
}
