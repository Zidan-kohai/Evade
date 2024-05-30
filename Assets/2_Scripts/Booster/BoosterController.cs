using DG.Tweening;
using GeekplaySchool;
using System;
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
    private int barrierUsedCount;

    [Header("Sensor")]
    [SerializeField] private GameObject sensorPrefab;
    [SerializeField] private float diactivateSensorTime;

    [Header("Mine")]
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float diactivateMineTime;

    [Header("Teleport")]
    [SerializeField] private TeleportPoint teleportPrefab;
    [SerializeField] private List<TeleportPoint> teleportsInstance = new();

    [Header("Meat")]
    [SerializeField] private int meatMaxUseCount = 3;
    [SerializeField] private int meatUsedCount = 0;
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
                    if (Geekplay.Instance.PlayerData.CurrentBoosterKeys[i] == booster.data.indexOnPlayer && Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(booster.data.indexOnPlayer).value != 0)
                    {
                        boosterItem[j].gameObject.SetActive(true);
                        boosterItem[j].image.sprite = booster.data.mainIcon;
                        int index = i;
                        int boosterIndex = j;
                        boosterItem[j].boostEvent.AddListener(() =>
                        {
                            if(Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(booster.data.indexOnPlayer).value == 0)
                            {
                                return;
                            }

                            Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(booster.data.indexOnPlayer).value--;

                            booster.boosterEvent?.Invoke();
                            DailyExerciseController.Instance.SetProgress(Days.Day3, 2);

                            if (Geekplay.Instance.PlayerData.BuyedBoosterID.GetByKey(booster.data.indexOnPlayer).value == 0)
                            {
                                boosterItem[boosterIndex].gameObject.SetActive(false);
                            }
                        });

                        result = true;
                        break;
                    }
                }
                if (result) break;
            }
        }

        foreach (MyDictionary booster in Boosters)
        {
            if (Geekplay.Instance.PlayerData.CurrentEquipedItemID == booster.data.indexOnPlayer && booster.data.type == SubjectType.Item)
            {

                boosterItem[5].gameObject.SetActive(true);
                boosterItem[5].image.sprite = booster.data.mainIcon;
                boosterItem[5].boostEvent.AddListener(() =>
                {
                    if (meatUsedCount >= meatMaxUseCount) return;

                    meatUsedCount++;
                    booster.boosterEvent?.Invoke();

                    if(meatUsedCount >= meatMaxUseCount) boosterItem[5].gameObject?.SetActive(false);
                });
                break;
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

        if (inputManager.GetIs4 && boosterItem[3].gameObject.activeSelf)
        {
            boosterItem[3].boostEvent?.Invoke();
        }

        if (inputManager.GetIs5 && boosterItem[4].gameObject.activeSelf)
        {
            boosterItem[4].boostEvent?.Invoke();
        }

        if (inputManager.GetIsG && boosterItem[5].gameObject.activeSelf)
        {
            boosterItem[5].boostEvent?.Invoke();
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

    #region Barrier

    public void Barrier()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Physics.Raycast(ray.origin, ray.direction, out hit, 100, barrierSpawnable, QueryTriggerInteraction.Ignore);
        GameObject barrierInstance = Instantiate(barrierPrefab, hit.point, realyPlayerTransform.rotation, null);

        DOTween.Sequence()
            .AppendInterval(barrierDiactivateTime)
            .AppendCallback(() => DiactivateBarrier(barrierInstance));

        DailyExerciseController.Instance.SetProgress(Days.Day3, 3);

        barrierUsedCount++;

        if(barrierUsedCount >= 10)
        {
            DailyExerciseController.Instance.SetProgress(Days.Day5, 3);
        }
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

    #region Mine
    public void Mine()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Physics.Raycast(ray.origin, ray.direction, out hit, 100, barrierSpawnable, QueryTriggerInteraction.Ignore);
        GameObject barrierInstance = Instantiate(minePrefab, hit.point, realyPlayerTransform.rotation, null);

        DOTween.Sequence()
            .AppendInterval(diactivateMineTime)
            .AppendCallback(() => DiactivateMine(minePrefab));
    }

    private void DiactivateMine(GameObject mine)
    {
        if(mine != null)
            Destroy(mine);
    }
    #endregion

    #region Teleport
    public void TeleportActivate()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Physics.Raycast(ray.origin, ray.direction, out hit, 100, barrierSpawnable, QueryTriggerInteraction.Ignore);
        TeleportPoint teleport = Instantiate(teleportPrefab, hit.point, realyPlayerTransform.rotation, null);
        teleport.enterAction += Teleport;
        teleportsInstance.Add(teleport);
    }

    public void Teleport(TeleportPoint point, IPlayer player)
    {
        int rand = UnityEngine.Random.Range(0, teleportsInstance.Count);
        if(point == teleportsInstance[rand])
        {
            Teleport(point, player);
            return;
        }

        player.Teleport(teleportsInstance[rand].transform.position);
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