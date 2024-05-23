using GeekplaySchool;
using System;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private GameObject spotLight;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private GameObject nightVisionGoggles;

    [SerializeField] private event Action currentLight;

    private void Start()
    {
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
        spotLight.SetActive(!spotLight.activeSelf);
    }

    private void PointLight()
    {
        pointLight.SetActive(!pointLight.activeSelf);
    }

    private void NightVision()
    {
        if (nightVisionGoggles.activeSelf)
        {
            nightVisionGoggles.SetActive(false);
            RenderSettings.fog = true;
        }
        else
        {
            nightVisionGoggles.SetActive(true);
            RenderSettings.fog = false;
        }
    }
}
