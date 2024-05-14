using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();


    public void HeadphoneBuff()
    {
        foreach (Enemy enemy in enemies) 
        {
            enemy.IncreaseSoundZone();
        }
    }
}
