using GeekplaySchool;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private List<Boostertem> boosterItem;
    [SerializeField] private List<ShopItemData> Boosters;


    private void Start()
    {
        //Need refactoring
        for (int i = 0; i < Geekplay.Instance.PlayerData.CurrentBoosterKeys.Count; i++)
        {
            if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == -1)
            {
                continue;
            }

            for (int j = i; j < boosterItem.Count; j++)
            {
                bool result = false;
                foreach (ShopItemData booster in Boosters)
                {
                    if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == booster.indexOnPlayer)
                    {
                        boosterItem[j].gameObject.SetActive(true);
                        boosterItem[j].image.sprite = booster.mainIcon;
                        result = true;
                        break;
                    }
                }
                if (result) break;
            }
        }
    }

    public void Update()
    {
        if(inputManager.GetIs1 && boosterItem[0].gameObject.activeSelf)
        {
            boosterItem[0].boostEvent?.Invoke();
        }

        if (inputManager.GetIs2 && boosterItem[1].gameObject.activeSelf)
        {
            boosterItem[1].boostEvent?.Invoke();
        }

        if (inputManager.GetIs3 && boosterItem[2].gameObject.activeSelf)
        {
            boosterItem[2].boostEvent?.Invoke();
        }
    }
}

[Serializable] 
public class Boostertem
{
    public GameObject gameObject;
    public Image image;
    public Image selecteble;
    public UnityEvent boostEvent;
}