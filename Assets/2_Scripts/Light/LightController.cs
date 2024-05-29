using GeekplaySchool;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<Light> globalLights;

    [Header("SpotLight")]
    [SerializeField] private Light spotLight;

    [Header("PointLight")]
    [SerializeField] private Light pointLight;
    [SerializeField] private int pointAreaOnLightOff;
    [SerializeField] private int pointAreaOnLightOn;

    [Header("NightVisionGoggles")]
    [SerializeField] private GameObject nightVisionGoggles;

    [SerializeField] private event Action currentLight;

    private bool isNight = false;

    private void Start()
    {
        int rand = UnityEngine.Random.Range(0, 6);

        if(rand >= 3)
        {
            isNight = true;
            pointLight.gameObject.SetActive(true);
            DisableGlobalLight();
        }
        else
        {
            pointLight.gameObject.SetActive(false);
            ActivateGlobalLight();
        }
        
        

        if (Geekplay.Instance.PlayerData.CurrentEquipedLightID == 1)
        {
            currentLight += SpotLight;
        }
        else if(Geekplay.Instance.PlayerData.CurrentEquipedLightID == 2)
        {
            currentLight += PointLight;
        }
        else if(Geekplay.Instance.PlayerData.CurrentEquipedLightID == 3)
        {
            currentLight += NightVision;
        }
    }


    private void Update()
    {
        if(inputManager.GetIsF)
        {
            currentLight?.Invoke();
        }
    }

    private void SpotLight()
    {
        spotLight.gameObject.SetActive(!spotLight.gameObject.activeSelf);
    }

    private void PointLight()
    {
        if(isNight)
        {
            pointLight.range = pointLight.range == pointAreaOnLightOff ? pointAreaOnLightOn : pointAreaOnLightOff;
        }
        else
        {
            pointLight.gameObject.SetActive(!pointLight.gameObject.activeSelf);
        }
    }


    private void NightVision()
    {
        if (nightVisionGoggles.activeSelf)
        {
            nightVisionGoggles.SetActive(false);

            if (isNight)
            {
                DisableGlobalLight();
            }
        }
        else
        {
            if(isNight)
            {
                ActivateGlobalLight();
            }
            nightVisionGoggles.SetActive(true);
        }
    }

    private void DisableGlobalLight()
    {
        foreach (var item in globalLights)
        {
            item.enabled = false;
        }
    }

    private void ActivateGlobalLight()
    {
        foreach (var item in globalLights)
        {
            item.enabled = true;
        }
    }
}
