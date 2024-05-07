using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessory : MonoBehaviour
{
    private static int skineIndex;


    [SerializeField] private List<Accessory> headphonesSkine;

    public static void ChangeCurrentSkineIndex(int index)
    {
        skineIndex = index;
    }

    private void Awake()
    {
        WearSkine();
    }

    private void WearSkine()
    {
        foreach (var item in headphonesSkine)
        {
            if(item.index == skineIndex)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}