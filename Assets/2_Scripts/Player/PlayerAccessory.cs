using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessory : MonoBehaviour
{

    [SerializeField] private List<Accessory> headphonesSkine;


    private void Start()
    {
        WearSkine();
    }

    private void WearSkine()
    {
        foreach (var item in headphonesSkine)
        {
            if(item.index == Geekplay.Instance.PlayerData.CurrentEquipedAccessoryID)
            {
                item.gameObject.SetActive(true);
                item.Event?.Invoke();
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}