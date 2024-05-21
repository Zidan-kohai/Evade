using GeekplaySchool;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private GameObject spotLight;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private GameObject nightVisionGoggles;

    [SerializeField] private GameObject currentLight;

    private void Start()
    {
        if (Geekplay.Instance.PlayerData.CurrentEquipedLightID == 1)
        {
            currentLight = spotLight;
        }
        else if(Geekplay.Instance.PlayerData.CurrentEquipedLightID == 2)
        {
            currentLight = pointLight;
        }
        else if(Geekplay.Instance.PlayerData.CurrentEquipedLightID == 3)
        {
            currentLight = nightVisionGoggles;
        }
    }


    private void Update()
    {
        if(inputManager.GetIsF)
        {
            currentLight.SetActive(!currentLight.activeSelf);
        }
    }
}
