using Cinemachine;
using DG.Tweening;
using GeekplaySchool;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHumanoid, ISee, IMove, IRealyPlayer
{
    //I Refactoring it as soon as i can
    [Header("managers")]
    [SerializeField] private GameplayController gamelpayController;

    [Header("Movement")]
    [SerializeField] private float startSpeedOnPlayerUp = 50f;
    [SerializeField] private float maxSpeedOnPlayerUp = 80f;
    [SerializeField] private float startSpeedOnPlayerFall = 20;
    [SerializeField] private float maxSpeedOnPlayerFall = 30;
    [SerializeField] private float currrentSpeed = 50f;
    [SerializeField] private int speedScaleFactor = 3;
    [SerializeField] private int slowFactor = 3;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundChekcRaycastOrigin;
    [SerializeField] private float groundChekcRaycastHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isOnGround;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private GameObject visualHandler;
    [SerializeField] private bool isTeleport;

    [Header("State")]
    [SerializeField] private PlayerState state;
    [SerializeField] private float timeToUpFromFall;
    [SerializeField] private float timeToDeathFromFall;
    [SerializeField] private float lostedTimeFromFallToUp;
    [SerializeField] private float lostedTimeFromFallToDeath;

    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ReachArea reachArea;
    [SerializeField] private ActionHandler actionUI;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform playerVisual;
    private CharacterController characterController;

    [Header("Humanoids")]
    private List<IEnemy> enemies = new List<IEnemy>();
    private List<IPlayer> players = new List<IPlayer>();

    [Header("Interactive")]
    [SerializeField] private float distanceToHelp;
    [SerializeField] private bool isCarry = false;
    [SerializeField] private Transform carriedTransform;
    [SerializeField] private IPlayer carriedPlayer;

    [Header("General")]
    [SerializeField] private int helpCount;
    [SerializeField] private int moneyMultiplierFactor = 1;
    [SerializeField] private int experienceMultiplierFactor = 1;
    [SerializeField] private Bait baitPrefab;
    private Action<IPlayer> playerDeathEvent;
    private string name = "Вы";
    private float livedTime = 0;
    private Coroutine coroutine;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        reachArea.SetISee(this);
        animationController.SetIMove(this);
        GameplayController.AddRealyPlayerST(this);
        BuffHandler.AddRealyPlayerST(this);
        BoosterController.AddRealyPlayerST(this);
    }

    private void Update()
    {

        if (state == PlayerState.Death || state == PlayerState.Carried) return;

        livedTime += Time.deltaTime;

        #region Movement

        Rotate();

        Move();

        CheckGround();

        Jump();

        ApplyGravity();

        characterController.Move(velocity * Time.deltaTime);

        #endregion

        switch (state)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Walk:
                break;

            case PlayerState.Fall:
                OnFall();
                break;

            case PlayerState.Death:
                break;
        }


        if (isCarry && inputManager.GetIsT)
            PutPlayer();

        if (state != PlayerState.Fall && state != PlayerState.Death)
            Help();
    }

    public void CreateBait()
    {
        Instantiate(baitPrefab, transform.position + visualHandler.transform.forward * 5 + new Vector3(0, 15, 0), Quaternion.Euler(transform.eulerAngles), null);
    }
    public void Carried(Transform point, CinemachineVirtualCamera virtualCamera)
    {
        ChangeState(PlayerState.Carried);
        playerVisual.position = point.transform.position;
        playerVisual.parent = point.transform;
        playerVisual.localEulerAngles = Vector3.zero;
        animationController.Carried();

        CameraConrtoller.PlayerCarriedST(virtualCamera);
    }

    public void GetDownOnGround()
    {
        transform.position = playerVisual.transform.position;
        playerVisual.parent = transform;
        playerVisual.localPosition = new Vector3(0, 1, 0);
        playerVisual.localEulerAngles = Vector3.zero;
        animationController.PutPlayer();

        ChangeState(PlayerState.Fall);
    }

    public bool GetIsTeleport()
    {
        return isTeleport;
    }

    public void Teleport(Vector3 teleportPosition)
    {
        isTeleport = true;
        characterController.enabled = false;
        transform.position = teleportPosition;

        DOTween.Sequence()
            .AppendInterval(0.3f)
            .AppendCallback(() => 
            {
                characterController.enabled = true;
                isTeleport = false;
            });  
    }

    public void SetTimeToUp(int deacreaseFactor)
    {
        timeToUpFromFall /= deacreaseFactor;
    }

    public string GetName() => name;

    public int GetEarnedMoney()
    {
        int earnedMoney = (helpCount * 10) + 25;

        earnedMoney *= moneyMultiplierFactor;

        return earnedMoney;
    }

    public int GetEarnedExperrience()
    {
        int earnedExperience = (helpCount * 10) + 10;

        earnedExperience *= experienceMultiplierFactor;

        return earnedExperience;
    }

    public void SetMoneyMulltiplierFactor(int value)
    {
        moneyMultiplierFactor = value;
    }

    public void SubscribeOnDeath(Action<IPlayer> onPlayerDeath)
    {
        playerDeathEvent += onPlayerDeath;
    }

    public void SetExperienceMulltiplierFactor(int value)
    {
        experienceMultiplierFactor = value;
    }

    public float SetMaxSpeedOnFall(int value, bool isFactor = true)
    {
        //increase maximum Fall speed by percentage(increaseFactor)
        float result;
        if(isFactor)
        {
            result = (maxSpeedOnPlayerFall * value) / 100;
            maxSpeedOnPlayerFall = maxSpeedOnPlayerFall + result;
        }
        else
        {
            result = value;
            maxSpeedOnPlayerFall = maxSpeedOnPlayerFall + result;
        }

        return result;
    }

    public float SetMaxSpeedOnUp(int value, bool isFactor = true)
    {
        //increase maximum Up speed by percentage(increaseFactor)
        float result;
        if(isFactor)
        {
            result = (maxSpeedOnPlayerUp * value) / 100;
            maxSpeedOnPlayerUp = maxSpeedOnPlayerUp + result;
        }
        else
        {
            result = value;
            maxSpeedOnPlayerUp = maxSpeedOnPlayerUp + result;
        }

        return result;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int GetHelpCount() => helpCount;

    public float GetSurvivedTime() => livedTime;

    public bool IsFallOrDeath()
    {
        return state == PlayerState.Fall || state == PlayerState.Death;
    }

    public bool IsFall()
    {
        return state == PlayerState.Fall;
    }

    public bool IsDeath()
    {
        return state == PlayerState.Death;
    }

    [ContextMenu("Fall")]
    public void Fall()
    {
        if (state == PlayerState.Death) return;

        if(isCarry)
        {
            PutPlayer();
        }

        ChangeState(PlayerState.Fall);
    }

    public float Raising()
    {
        ChangeState(PlayerState.Raising);

        if (coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(Wait(0.5f, () =>
        {
            ChangeState(PlayerState.Fall);
        }));

        lostedTimeFromFallToUp -= Time.deltaTime;

        float raisingPercent = GetPercentOfRaising();

        actionUI.ShowHelpingUISlider(raisingPercent);

        if (lostedTimeFromFallToUp <= 0)
        {
            ChangeState(PlayerState.Idle);
            actionUI.DisableHelpingUIHandler();
            StopCoroutine(coroutine);
        }

        return Mathf.Abs(lostedTimeFromFallToUp / timeToUpFromFall - 1);
    }

    public float GetPercentOfRaising()
    {
        return Mathf.Abs(lostedTimeFromFallToUp / timeToUpFromFall - 1);
    }

    public void AddHumanoid(IHumanoid IHumanoid)
    {
        if(IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemies.Add(enemy);
        }
        else if(IHumanoid.gameObject.TryGetComponent(out IPlayer player) && player != this && !IHumanoid.gameObject.TryGetComponent(out Bait bait))
        {
            players.Add(player);
        }
    }

    public void RemoveHumanoid(IHumanoid IHumanoid)
    {
        if (IHumanoid.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemies.Remove(enemy);
        }
        else if (IHumanoid.gameObject.TryGetComponent(out IPlayer player))
        {
            players.Remove(player);
        }
    }

    public float MoveSpeed()
    {
        Vector3 move = new Vector3(velocity.x, 0, velocity.z);

        return move.magnitude;
    }

    public bool IsJump()
    {
        return !isOnGround && velocity.y != 0;
    }

    private void OnFall()
    {
        lostedTimeFromFallToDeath -= Time.deltaTime;

        actionUI.ChangeDeathPercent(lostedTimeFromFallToDeath / timeToDeathFromFall);

        if (lostedTimeFromFallToDeath <= 0)
        {
            ChangeState(PlayerState.Death);
        }
    }

    private void Rotate()
    {
        switch(CameraConrtoller.GetCameraStateST())
        {
            case CameraState.First:
                FirstPersonRotate();
                break;
            case CameraState.Third:
                ThirdPersonRotate();
                break;
        }

    }

    private void ThirdPersonRotate()
    {
        Vector3 velocityXY = new Vector3(velocity.x, 0, velocity.z);

        if (velocityXY.magnitude < 0.01f) return;

        Quaternion targetRotation = Quaternion.LookRotation(velocityXY.normalized, Vector3.up);
        visualHandler.transform.rotation = Quaternion.Lerp(visualHandler.transform.rotation, targetRotation, Time.deltaTime * mouseSensitivity);
    }

    private void FirstPersonRotate()
    {
        float mouseX = inputManager.GetMouseDeltaX * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Move()
    {
        float startSpeed = startSpeedOnPlayerUp;
        float maxSpeed = maxSpeedOnPlayerUp;

        if (state == PlayerState.Fall)
        {
            startSpeed = startSpeedOnPlayerFall;
            maxSpeed = maxSpeedOnPlayerFall;
        }

        float moveHorizontal = inputManager.GetMoveHorizontal;
        float moveVertical = inputManager.GetMoveVertical;

        
        Vector3 move = Camera.main.transform.right * moveHorizontal + Camera.main.transform.forward * moveVertical;

        move.Normalize();

        velocity.x = move.x * currrentSpeed;
        velocity.z = move.z * currrentSpeed;

        if(moveHorizontal == 0 && moveVertical == 0)
        {
            currrentSpeed = 0;

            if(state != PlayerState.Fall && state != PlayerState.Carried)
                ChangeState(PlayerState.Idle);
        }
        else
        {
            currrentSpeed += Time.deltaTime * speedScaleFactor;
            currrentSpeed = Mathf.Clamp(currrentSpeed, startSpeed, maxSpeed);

            if (state != PlayerState.Fall && state != PlayerState.Carried)
                ChangeState(PlayerState.Walk);
        }

        actionUI.ChangeSpeed(currrentSpeed);
    }

    private void Jump()
    {
        if (inputManager.GetSpace && isOnGround && canJump)
        {
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            velocity.y += jumpVelocity;
            isOnGround = false;
            currrentSpeed += Time.deltaTime * speedScaleFactor * 30;
            currrentSpeed = Mathf.Clamp(currrentSpeed, 0, maxSpeedOnPlayerUp);
        }
    }

    private void ApplyGravity()
    {
        if (!isOnGround)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }
    }

    private void CheckGround()
    {
        isOnGround = Physics.Raycast(groundChekcRaycastOrigin.position, Vector3.down, groundChekcRaycastHeight, groundLayer);
    }

    private void ChangeState(PlayerState newState)
    {
        //if state and newState are equal we don`t do anything
        //if current state is fall we can switch only to the idle, death or carried state
        //if current state is carried we can switch only to the fall
        //if current state is death we don`t do anything
        if (state == newState
            ||

            ((state == PlayerState.Fall)
            && (newState != PlayerState.Idle) && (lostedTimeFromFallToUp > 0)
            && (newState != PlayerState.Death))
            && (newState != PlayerState.Carried)

            ||
            (state == PlayerState.Carried)
            && newState != PlayerState.Fall

            || state == PlayerState.Death) return;

        state = newState;

        switch(state)
        {
            case PlayerState.Idle:
                canJump = true;
                animationController.Up();
                CameraConrtoller.PlayerUpST();
                break;
            case PlayerState.Walk:
                canJump = true;
                break;
            case PlayerState.Fall:
                canJump = false;
                PlayerStateShower.ShowState(state);
                CameraConrtoller.PlayerFallST();
                animationController.Fall();
                lostedTimeFromFallToUp = timeToUpFromFall;
                lostedTimeFromFallToDeath = timeToDeathFromFall;
                break;
            case PlayerState.Raising:
                canJump = false;
                break;
            case PlayerState.Carry:
                animationController.Carry();
                break;
            case PlayerState.Carried:
                animationController.Carried();
                break;
            case PlayerState.Death:
                canJump = false;
                Death();
                break;
        }
    }

    private void Help()
    {
        if (CanHelp(out IPlayer player))
        {
            actionUI.ShowHelpingUIManual();
            actionUI.ShowCarryExplain();

            if(inputManager.GetIsT && !isCarry)
            {
                Carry(player);
            }
            else if (inputManager.GetIsE)
            {
                float helpPercent = player.Raising();
                actionUI.FilHelpigUI(helpPercent);

                if(helpPercent >= 1)
                {
                    helpCount++;

                    DailyExerciseController.Instance.SetProgress(Days.Day1, 3);
                    DailyExerciseController.Instance.SetProgress(Days.Day4, 2);
                    DailyExerciseController.Instance.SetProgress(Days.Day5, 2);

                    Geekplay.Instance.PlayerData.HelpCount++;
                    Geekplay.Instance.SetLeaderboard(Helper.HelpLeaderboardName, Geekplay.Instance.PlayerData.HelpCount);
                    Geekplay.Instance.Save();
                }
            }
        }
        else
        {
            actionUI.DisableHelpingUIHandler();
            actionUI.DisableCarryExplain();
        }
    }

    private bool CanHelp(out IPlayer player)
    {
        for(int i = 0; i < players.Count; i++)
        {
            float distanceToPlayer = (players[i].GetTransform().position - transform.position).magnitude;
            if(players[i].IsFall() && distanceToPlayer < distanceToHelp)
            {
                player = players[i];
                return true;
            }
        }

        player = null;
        return false;
    }

    private void Carry(IPlayer player)
    {
        actionUI.ShowPutExplain();
        isCarry = true;
        player.Carried(carriedTransform, virtualCamera);
        carriedPlayer = player;
        ChangeState(PlayerState.Carry);
    }

    private void PutPlayer()
    {
        actionUI.DisablePutExplain();
        actionUI.DisableCarryExplain();
        animationController.PutPlayer();
        carriedPlayer.GetDownOnGround();
        isCarry = false;
        carriedPlayer = null;
    }

    private void Death()
    {
        PlayerStateShower.ShowState(state);
        playerDeathEvent?.Invoke(this);
        gamelpayController.OnPlayerDeath(livedTime);
        animationController.Death();
    }

    private IEnumerator Wait(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

    //maybe i refactoring it in future
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        currrentSpeed -= Time.deltaTime * slowFactor;
        currrentSpeed = Mathf.Clamp(currrentSpeed, 0, 1000);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundChekcRaycastOrigin.position, Vector3.down * groundChekcRaycastHeight);
    }
}
