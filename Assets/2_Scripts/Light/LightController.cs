using GeekplaySchool;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<Light> globalLights;
    [SerializeField] private Color cameraColorOnDay;

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

        bool isfirstOrSecondScene = SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 3;

        if (isfirstOrSecondScene || (rand >= 3 && Geekplay.Instance.nightCount < 3))
        {
            Geekplay.Instance.nightCount++;
            isNight = true;
            pointLight.gameObject.SetActive(true);
            DisableGlobalLight();
        }
        else
        {
            Geekplay.Instance.nightCount = 0;
            pointLight.gameObject.SetActive(false);
            Camera.main.backgroundColor = cameraColorOnDay;
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
