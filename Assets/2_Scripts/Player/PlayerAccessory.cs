using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessory : MonoBehaviour
{
    private static int skineIndex;


    [SerializeField] private List<Accessory> headphonesSkine;


    private void Awake()
    {
        WearSkine();
    }

    public static void ChangeCurrentSkineIndex(int index)
    {
        skineIndex = index;
        Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID = skineIndex;
        Geekplay.Instance.Save();
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