using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpHeight = 2f; 
    public float gravity = -9.81f;

    [SerializeField] private InputManager inputManager;
    private CharacterController characterController;
    private float verticalRotation = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = inputManager.GetMouseDeltaX * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = inputManager.GetMouseDeltaY * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Ограничиваем поворот вверх и вниз
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        float moveHorizontal = inputManager.GetMoveHorizontal;
        float moveVertical = inputManager.GetMoveVertical;

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (inputManager.GetSpace && characterController.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            characterController.Move(Vector3.up * jumpVelocity * Time.deltaTime);
        }
        if (!characterController.isGrounded)
        {
            Gravity();
        }
    }

    public void Gravity()
    {
        characterController.Move(Vector3.up * gravity * Time.deltaTime);
    }
}
