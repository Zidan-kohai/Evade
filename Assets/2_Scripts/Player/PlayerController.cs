using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer, IHumanoid, ISee, IMove
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
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundChekcRaycastOrigin; 
    [SerializeField] private float groundChekcRaycastHeight; 
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private bool isOnGround;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private GameObject visualHandler;

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
    private CharacterController characterController;

    [Header("Humanoids")]
    private List<IEnemy> enemies = new List<IEnemy>();
    private List<IPlayer> players = new List<IPlayer>();

    [Header("Interactive")]
    [SerializeField] private float distanceToHelp;

    private Coroutine coroutine;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        reachArea.SetISee(this);
        animationController.SetIMove(this);

        Cursor.lockState = CursorLockMode.Locked;

        CameraConrtoller.AddCameraST(virtualCamera);
    }

    private void Update()
    {
        if (state == PlayerState.Death) return;

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

        if(state != PlayerState.Fall && state != PlayerState.Death)
            Help();
    }

    public Transform GetTransform()
    {
        return transform;
    }

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

    public void Fall()
    {
        if (state == PlayerState.Death) return;
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
        else if(IHumanoid.gameObject.TryGetComponent(out IPlayer player) && player != this)
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
        //First Person Controller
        //float mouseX = inputManager.GetMouseDeltaX * mouseSensitivity * Time.deltaTime;
        //transform.Rotate(Vector3.up * mouseX);

        //Third Person Conteroller
        
        Vector3 velocityXY = new Vector3(velocity.x, 0, velocity.z);

        if (velocityXY.magnitude < 0.01f) return;

        Quaternion targetRotation = Quaternion.LookRotation(velocityXY.normalized, Vector3.up);
        visualHandler.transform.rotation = Quaternion.Lerp(visualHandler.transform.rotation, targetRotation, Time.deltaTime * mouseSensitivity);
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

        velocity.x = move.x * currrentSpeed;
        velocity.z = move.z * currrentSpeed;

        if(moveHorizontal == 0 && moveVertical == 0)
        {
            currrentSpeed = 0;
            ChangeState(PlayerState.Idle);
        }
        else
        {
            currrentSpeed += Time.deltaTime * speedScaleFactor;
            currrentSpeed = Mathf.Clamp(currrentSpeed, startSpeed, maxSpeed);
            ChangeState(PlayerState.Walk);
        }

        actionUI.ChangeSpeed(currrentSpeed);
    }

    private void Jump()
    {
        if (inputManager.GetSpace && isOnGround)
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
        if (state == newState ||
            ((state == PlayerState.Fall) && (lostedTimeFromFallToUp > 0) && newState != PlayerState.Death)) return;

        state = newState;

        switch(state)
        {
            case PlayerState.Idle:
                animationController.Up();
                break;
            case PlayerState.Walk:
                break;
            case PlayerState.Fall:
                animationController.Fall();
                lostedTimeFromFallToUp = timeToUpFromFall;
                lostedTimeFromFallToDeath = timeToDeathFromFall;
                break;
            case PlayerState.Raising:
                break;
            case PlayerState.Death:
                Death();
                break;
        }
    }

    private void Help()
    {
        if (CanHelp(out IPlayer player))
        {
            actionUI.ShowHelpingUIManual();

            if (inputManager.GetIsE)
            {
                float helpPercent = player.Raising();
                actionUI.FilHelpigUI(helpPercent);
            }
        }
        else
        {
            actionUI.DisableHelpingUIHandler();
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

    private void Death()
    {
        gamelpayController.OnPlayerDeath();
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