using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer, IHumanoid,ISee
{
    [Header("Transform")]
    [SerializeField] private float startSpeedOnPlayerUp = 1f;
    [SerializeField] private float maxSpeedOnPlayerUp = 5f;
    [SerializeField] private float startSpeedOnPlayerFall = 0.5f;
    [SerializeField] private float maxSpeedOnPlayerFall = 1f;
    [SerializeField] private float currrentSpeed = 1f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float minVerticalRotateClamp = 100f;
    [SerializeField] private float maxVerticalRotateClamp = 100f;
    [SerializeField] private float verticalRotation = 0f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundChekcRaycastOrigin; 
    [SerializeField] private float groundChekcRaycastHeight; 
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private bool isOnGround;
    [SerializeField] private Vector3 velocity;

    [Header("State")]
    [SerializeField] private PlayerState state;
    [SerializeField] private float timeToUpFromFall;
    [SerializeField] private float timeToDeathFromFall;
    [SerializeField] private float lastedTimeFromFallToUp;
    [SerializeField] private float lastedTimeFromFallToDeath;

    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ReachArea reachArea;
    private CharacterController characterController;

    [Header("Humanoids")]
    private List<IEnemy> enemies = new List<IEnemy>();
    private List<IPlayer> players = new List<IPlayer>();

    [Header("Interactive")]
    [SerializeField] private float distanceToHelp;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        reachArea.SetISee(this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();

        if (state != PlayerState.Fall)
        {
            Move(startSpeedOnPlayerUp, maxSpeedOnPlayerUp);
        }
        else
        {
            Move(startSpeedOnPlayerFall, maxSpeedOnPlayerFall);
        }

        CheckGround();

        Jump();
        
        ApplyGravity();

        characterController.Move(velocity * Time.deltaTime);
        
        if(CanHelp(out IPlayer player) && inputManager.GetIsE)
        {
            player.Raising();
        }
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
        ChangeState(PlayerState.Fall);
    }

    public void Raising()
    {
        lastedTimeFromFallToUp -= Time.deltaTime;

        if (lastedTimeFromFallToUp <= 0)
        {
            ChangeState(PlayerState.Idle);
        }
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

    private void Rotate()
    {
        float mouseX = inputManager.GetMouseDeltaX * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = inputManager.GetMouseDeltaY * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotateClamp, maxVerticalRotateClamp);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void Move(float startSpeed, float maxSpeed)
    {
        float moveHorizontal = inputManager.GetMoveHorizontal;
        float moveVertical = inputManager.GetMoveVertical;

        
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        velocity.x = move.x * currrentSpeed;
        velocity.z = move.z * currrentSpeed;

        if(moveHorizontal == 0 && moveVertical == 0)
        {
            currrentSpeed = startSpeed;
            ChangeState(PlayerState.Idle);
        }
        else
        {
            currrentSpeed += Time.deltaTime;
            currrentSpeed = Mathf.Clamp(currrentSpeed, startSpeed, maxSpeed);
            ChangeState(PlayerState.Run);
        }
    }

    private void Jump()
    {
        if (inputManager.GetSpace && isOnGround)
        {
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            velocity.y += jumpVelocity;
            isOnGround = false;
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
            ((state == PlayerState.Fall) && 
            (newState == PlayerState.Raising || newState == PlayerState.Death))) return;

        state = newState;

        switch(state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Fall:
                Debug.Log("Fall");
                break;
            case PlayerState.Death: 
                break;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundChekcRaycastOrigin.position, Vector3.down * groundChekcRaycastHeight);
    }
}