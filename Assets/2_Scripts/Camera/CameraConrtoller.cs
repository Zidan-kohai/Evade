using Cinemachine;
using System;
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
    [SerializeField] private GameObject cameraSwitherHandler;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float minVerticalRotateClamp = -90f;
    [SerializeField] private float maxVerticalRotateClamp = 90f;
    [SerializeField] private float verticalRotation = 0f;
    [SerializeField] private CameraState currentState;
    [SerializeField] private CameraState stateOnUp;

    private void Awake()
    {
        switch (currentState)
        {
            case CameraState.First:
                firstPersonCamera.Priority = 2;
                AIPlayersCamera[0].Priority = 1;
                break;
            case CameraState.Third:
                firstPersonCamera.Priority = 1;
                AIPlayersCamera[0].Priority = 2;
                break;
        }

        instance = this;
    }

    private void Update()
    {
        switch (currentState)
        {
            case CameraState.First:
                FirstPersonConrtoller();
                break;
            case CameraState.Third:
                break;
        }
    }

    public static void AddCameraST(CinemachineVirtualCamera virtualCamera)
    {
        instance.AddCamera(virtualCamera);
    }

    public static CameraState GetCameraStateST() => instance.currentState;

    public static void ChangeStateST(CameraState newState)
    {
        instance.ChangeState(newState);
    }

    public static void PlayerFallST()
    {
        instance.PlayerFall();

    }

    public static void PlayerUpST()
    {
        instance.PlayerUp();
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

    public void NextState()
    {
        switch (currentState)
        {
            case CameraState.First:
                ChangeState(CameraState.Third);
                stateOnUp = currentState;
                break;
            case CameraState.Third:
                ChangeState(CameraState.First);
                stateOnUp = currentState;
                break;
        }
    }

    private void FirstPersonConrtoller()
    {
        float mouseY = inputManager.GetMouseDeltaY * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotateClamp, maxVerticalRotateClamp);
        firstPersonCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void PlayerFall()
    {
        stateOnUp = currentState;
        cameraSwitherHandler.SetActive(false);
        ChangeState(CameraState.Third);
    }

    private void PlayerUp()
    {
        ChangeState(stateOnUp);
        cameraSwitherHandler.SetActive(true);
    }

    private void ChangeState(CameraState newState)
    {
        if (newState == currentState) return;

        currentState = newState;

        switch (currentState)
        {
            case CameraState.First:
                firstPersonCamera.Priority = 2;
                AIPlayersCamera[0].Priority = 1;
                break;
            case CameraState.Third:
                firstPersonCamera.Priority = 1;
                AIPlayersCamera[0].Priority = 2;
                break;
        }
    }

    private void AddCamera(CinemachineVirtualCamera virtualCamera)
    {
        AIPlayersCamera.Add(virtualCamera);
    }
}
