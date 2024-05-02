using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraConrtoller : MonoBehaviour
{
    private static CameraConrtoller instance;

    [Header("Components")]
    [SerializeField] private Camera camera;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CinemachineVirtualCamera firstPersonCamera;
    [SerializeField] private List<CinemachineVirtualCamera> AIPlayersCamera;
    [SerializeField] private int currentAIPlayerCameraIndex;

    //[SerializeField] private float mouseSensitivity;
    //[SerializeField] private float minVerticalRotateClamp = -90f;
    //[SerializeField] private float maxVerticalRotateClamp = 90f; 
    //[SerializeField] private float verticalRotation = 0f;
    //[SerializeField] private CameraState state;

    private void Awake()
    {
        instance = this;
    }

    public static void AddCameraST(CinemachineVirtualCamera virtualCamera)
    {
        instance.AddCamera(virtualCamera);
    }

    private void AddCamera(CinemachineVirtualCamera virtualCamera)
    {
        AIPlayersCamera.Add(virtualCamera);
    }

    public void NextCamera()
    {
        AIPlayersCamera[currentAIPlayerCameraIndex].Priority = 1;

        currentAIPlayerCameraIndex = (currentAIPlayerCameraIndex + 1) % AIPlayersCamera.Count;

        AIPlayersCamera[currentAIPlayerCameraIndex].Priority = 2;
    }

    public void PreviousCamera()
    {
        AIPlayersCamera[currentAIPlayerCameraIndex].Priority = 1;

        currentAIPlayerCameraIndex = (currentAIPlayerCameraIndex - 1) < 0 ? AIPlayersCamera.Count - 1 : currentAIPlayerCameraIndex - 1;

        AIPlayersCamera[currentAIPlayerCameraIndex].Priority = 2;
    }
}
