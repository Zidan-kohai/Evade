using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private float startSpeed = 1f;
    [SerializeField] private float currrentSpeed = 1f;
    [SerializeField] private float maxSpeed = 5f;
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

    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();

        Move();

        CheckGround();

        Jump();
        
        ApplyGravity();

        characterController.Move(velocity * Time.deltaTime);
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

    private void Move()
    {
        float moveHorizontal = inputManager.GetMoveHorizontal;
        float moveVertical = inputManager.GetMoveVertical;

        
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        velocity.x = move.x * currrentSpeed;
        velocity.z = move.z * currrentSpeed;

        if(moveHorizontal == 0 && moveVertical == 0)
        {
            currrentSpeed = startSpeed;
        }
        else
        {
            currrentSpeed += Time.deltaTime;
            currrentSpeed = Mathf.Clamp(currrentSpeed, startSpeed, maxSpeed);
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

    public void ApplyGravity()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundChekcRaycastOrigin.position, Vector3.down * groundChekcRaycastHeight);
    }
}
