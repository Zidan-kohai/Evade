using DG.Tweening;
using GeekplaySchool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterController : MonoBehaviour
{
    private static BoosterController instance;


    [SerializeField] private InputManager inputManager;

    [SerializeField] private List<BoosterItem> boosterItem;
    [SerializeField] private List<MyDictionary> Boosters;

    [Header("Realy Player")]
    [SerializeField] private IRealyPlayer realyPlayer;
    [SerializeField] private Transform realyPlayerTransform;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Cola")]
    [SerializeField] private float colaDiactivateTime;
    private Sequence ColaSequence;

    [Header("WandOfLight")]
    [SerializeField] private GameObject WandOfLightPrefab;

    [Header("Barrier")]
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private float barrierDiactivateTime;
    [SerializeField] private LayerMask barrierSpawnable;

    [Header("Sensor")]
    [SerializeField] private GameObject sensorPrefab;
    [SerializeField] private float diactivateSensorTime;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

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
                foreach (MyDictionary booster in Boosters)
                {
                    if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == booster.data.indexOnPlayer)
                    {

                        boosterItem[j].gameObject.SetActive(true);
                        boosterItem[j].image.sprite = booster.data.mainIcon;
                        boosterItem[j].boostEvent.AddListener(() => booster.boosterEvent?.Invoke());

                        result = true;
                        break;
                    }
                }
                if (result) break;
            }
        }
    }

    private void Update()
    {
        if (realyPlayer.IsFallOrDeath()) return;

        if (inputManager.GetIs1 && boosterItem[0].gameObject.activeSelf)
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

    public static void AddRealyPlayerST(IRealyPlayer realyPlayer)
    {
        instance.AddRealyPlayer(realyPlayer);
    }

    private void AddRealyPlayer(IRealyPlayer realyPlayer)
    {
        this.realyPlayer = realyPlayer;
    }

    #region Cola

    public void ColaBoost()
    {
        ColaSequence.Kill();

        float deltaUp = realyPlayer.SetMaxSpeedOnUp(50);
        float deltaFall = realyPlayer.SetMaxSpeedOnFall(50);

        ColaSequence = DOTween.Sequence()
            .AppendInterval(colaDiactivateTime)
            .OnKill(() => ColaBoostDiactivate(deltaUp, deltaFall));

    }

    private void ColaBoostDiactivate(float deltaUp, float deltaFall)
    {
        realyPlayer.SetMaxSpeedOnUp((int)-deltaUp, false);
        realyPlayer.SetMaxSpeedOnFall((int)-deltaFall, false);
    }

    #endregion

    #region WangOfLight

    public void WandOfLightBoost()
    {
        Instantiate(WandOfLightPrefab, realyPlayerTransform.position + realyPlayerTransform.forward * 5 + new Vector3(0, 5, 0), Quaternion.identity, null);
    }

    #endregion

    #region BarrierDiactivate

    public void Barrier()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Physics.Raycast(ray.origin, ray.direction, out hit, 100, barrierSpawnable, QueryTriggerInteraction.Ignore);
        GameObject barrierInstance = Instantiate(barrierPrefab, hit.point, realyPlayerTransform.rotation, null);

        DOTween.Sequence()
            .AppendInterval(barrierDiactivateTime)
            .AppendCallback(() => DiactivateBarrier(barrierInstance));
    }

    private void DiactivateBarrier(GameObject barrierInstance)
    {
        Destroy(barrierInstance);
    }

    #endregion

    #region Sensor
    public void Sensor()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Physics.Raycast(ray.origin, ray.direction, out hit, 100, barrierSpawnable, QueryTriggerInteraction.Ignore);
        GameObject barrierInstance = Instantiate(sensorPrefab, hit.point, realyPlayerTransform.rotation, null);

        DOTween.Sequence()
            .AppendInterval(diactivateSensorTime)
            .AppendCallback(() => DiactivateSensor(sensorPrefab));
    }

    private void DiactivateSensor(GameObject sensor)
    {
        Destroy(sensor);
    }
    
    #endregion

    [Serializable]
    public class MyDictionary
    {
        public ShopItemData data;
        public UnityEvent boosterEvent;
    }
}

[Serializable] 
public class BoosterItem
{
    public GameObject gameObject;
    public Image image;

    [HideInInspector]
    public UnityEvent boostEvent;
}