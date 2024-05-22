using Cinemachine;
using GeekplaySchool;
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
    [SerializeField] private CinemachineVirtualCamera thirdPlayer;
    [SerializeField] private List<IPlayerCamera> AIPlayersCamera;
    [SerializeField] private int currentAIPlayerCameraIndex;
    [SerializeField] private GameObject cameraSwitherHandler;
    [SerializeField] private bool canSwitchCamera = true;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float minVerticalRotateClamp = -90f;
    [SerializeField] private float maxVerticalRotateClamp = 90f;
    [SerializeField] private float verticalRotation = 0f;
    [SerializeField] private CameraState currentState;
    [SerializeField] private CameraState stateOnUp;
    [SerializeField] private CinemachineVirtualCamera carryPlayerCamera;

    private void Awake()
    {
        if (!Geekplay.Instance.mobile)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        switch (currentState)
        {
            case CameraState.First:
                firstPersonCamera.Priority = 2;
                thirdPlayer.Priority = 1;
                break;
            case CameraState.Third:
                firstPersonCamera.Priority = 1;
                thirdPlayer.Priority = 2;
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

        if (inputManager.GetIsTab && canSwitchCamera)
            NextState();
    }

    public static void AddCameraST(AIPlayer player, CinemachineVirtualCamera virtualCamera)
    {
        instance.AddCamera(player, virtualCamera);
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

    public static void PlayerCarriedST(CinemachineVirtualCamera virtualCamera)
    {
        instance.PlayerCarried(virtualCamera);
        Debug.Log("Virtual camera Change");
    }

    public void NextCamera()
    {
        
        if(CheckIsAllPlayerDie()) return;

        if (AIPlayersCamera[(currentAIPlayerCameraIndex + 1) % AIPlayersCamera.Count].IPlayer.IsDeath())
        {
            currentAIPlayerCameraIndex = (currentAIPlayerCameraIndex + 1) % AIPlayersCamera.Count;
            NextCamera();
            return;
        }

        AIPlayersCamera[currentAIPlayerCameraIndex].VirtualCamera.Priority = 1;

        currentAIPlayerCameraIndex = (currentAIPlayerCameraIndex + 1) % AIPlayersCamera.Count;

        AIPlayersCamera[currentAIPlayerCameraIndex].VirtualCamera.Priority = 2;
    }

    public void PreviousCamera()
    {
        if (CheckIsAllPlayerDie()) return;

        if (AIPlayersCamera[currentAIPlayerCameraIndex - 1 < 0 ? AIPlayersCamera.Count - 1 : 
            currentAIPlayerCameraIndex - 1].IPlayer.IsDeath())
        {
            currentAIPlayerCameraIndex = (currentAIPlayerCameraIndex - 1) < 0 ? AIPlayersCamera.Count - 1 : currentAIPlayerCameraIndex - 1;
            NextCamera();
            return;
        }

        AIPlayersCamera[currentAIPlayerCameraIndex].VirtualCamera.Priority = 1;

        currentAIPlayerCameraIndex = (currentAIPlayerCameraIndex - 1) < 0 ? AIPlayersCamera.Count - 1 : currentAIPlayerCameraIndex - 1;

        AIPlayersCamera[currentAIPlayerCameraIndex].VirtualCamera.Priority = 2;
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

    private bool CheckIsAllPlayerDie()
    {
        bool result = true;

        foreach (var item in AIPlayersCamera)
        {
            if (!item.IPlayer.IsDeath())
            {
                result = false;
            }
        }

        return result;
    }

    private void FirstPersonConrtoller()
    {
        float mouseY = inputManager.GetMouseDeltaY * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotateClamp, maxVerticalRotateClamp);
        firstPersonCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void PlayerCarried(CinemachineVirtualCamera virtualCamera)
    {
        carryPlayerCamera = virtualCamera;
        carryPlayerCamera.Priority = 3;
    }

    private void PlayerFall()
    {
        if (carryPlayerCamera != null)
        {
            carryPlayerCamera.Priority = 1;
            carryPlayerCamera = null;
        }

        canSwitchCamera = false;
        stateOnUp = currentState;
        cameraSwitherHandler.SetActive(false);
        ChangeState(CameraState.Third);
    }

    private void PlayerUp()
    {
        canSwitchCamera = true;
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
                thirdPlayer.Priority = 1;
                break;
            case CameraState.Third:
                firstPersonCamera.Priority = 1;
                thirdPlayer.Priority = 2;
                break;
        }
    }

    private void AddCamera(AIPlayer player, CinemachineVirtualCamera virtualCamera)
    {
        AIPlayersCamera.Add(new IPlayerCamera
        {
           IPlayer = player,
           VirtualCamera = virtualCamera 
        });
    }
}
