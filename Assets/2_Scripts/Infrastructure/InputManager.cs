using DG.Tweening;
using GeekplaySchool;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private MyButton spaceButton;
    [SerializeField] private MyButton isEButton;
    [SerializeField] private MyButton isTabButton;
    [SerializeField] private MyButton isTButton;
    [SerializeField] private MyButton isT2Button;
    [SerializeField] private MyButton isFButton;
    [SerializeField] private MyButton is1Button;
    [SerializeField] private MyButton is2Button;
    [SerializeField] private MyButton is3Button;
    [SerializeField] private MyButton is4Button;
    [SerializeField] private MyButton is5Button;
    [SerializeField] private MyButton is6Button;

    private float mouseDeltaX;
    private float mouseDeltaY;
    private float moveHorizontal;
    private float moveVertical;
    private bool space;
    private bool isE;
    private bool isT;
    private bool isTab;
    private bool isFClick;
    private bool is1;
    private bool is2;
    private bool is3;
    private bool is4;
    private bool is5;
    private bool is6;
    public float GetMouseDeltaX => mouseDeltaX;
    public float GetMouseDeltaY => mouseDeltaY;
    public float GetMoveHorizontal => moveHorizontal;
    public float GetMoveVertical => moveVertical;
    public bool GetSpace => space;
    public bool GetIsE => isE;
    public bool GetIsTab => isTab;
    public bool GetIsT => isT;
    public bool GetIsF => isFClick;
    public bool GetIs1 => is1;
    public bool GetIs2 => is2;
    public bool GetIs3 => is3;
    public bool GetIs4 => is4;
    public bool GetIs5 => is5;
    public bool GetIs6 => is6;

    private void Start()
    {
        if (Geekplay.Instance.mobile)
        {
            SubscribeMobileInput();
        }
    }

    private void Update()
    {
        if (!Geekplay.Instance.mobile)
        {
            DeskInput();
        }
        else
        {
            MobileInput();
        }
        
    }

    private void MobileInput()
    {
        mouseDeltaX = swipeDetector.swipeDelta.x;

        mouseDeltaY = swipeDetector.swipeDelta.y;

        moveHorizontal = joystick.Horizontal;

        moveVertical = joystick.Vertical;
    }

    private void SubscribeMobileInput()
    {
        spaceButton.OnHandDown += () => space = true;
        spaceButton.OnHandUp += () => space = false;

        isEButton.OnHandDown += () => isE = true;
        isEButton.OnHandUp += () => isE = false;

        isTabButton.onClick.AddListener(() =>
        {
            isTab = true;
            StartCoroutine(WaitFrame(() => isTab = false));
        });

        isTButton.onClick.AddListener(() =>
        {
            isT = true;
            StartCoroutine(WaitFrame(() => isT = false));
        });

        isT2Button.onClick.AddListener(() =>
        {
            isT = true;
            StartCoroutine(WaitFrame(() => isT = false));
        });

        isFButton.onClick.AddListener(() =>
        {
            isFClick = true;
            StartCoroutine(WaitFrame(() => isFClick = false));
        });

        is1Button.onClick.AddListener(() =>
        {
            is1 = true;
            StartCoroutine(WaitFrame(() => is1 = false));
        });

        is2Button.onClick.AddListener(() =>
        {
            is2 = true;
            StartCoroutine(WaitFrame(() => is2 = false));
        });

        is3Button.onClick.AddListener(() =>
        {
            is3 = true;
            StartCoroutine(WaitFrame(() => is3 = false));
        });

        is4Button.onClick.AddListener(() =>
        {
            is4 = true;
            StartCoroutine(WaitFrame(() => is4 = false));
        });

        is5Button.onClick.AddListener(() =>
        {
            is5 = true;
            StartCoroutine(WaitFrame(() => is5 = false));
        });

        is6Button.onClick.AddListener(() =>
        {
            is6 = true;
            StartCoroutine(WaitFrame(() => is6 = false));
        });
    }

    private void DeskInput()
    {
        mouseDeltaX = Input.GetAxis("Mouse X");

        mouseDeltaY = Input.GetAxis("Mouse Y");

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        space = Input.GetButtonDown("Jump");

        isE = Input.GetKey(KeyCode.E);

        isTab = Input.GetKeyDown(KeyCode.Tab);

        isT = Input.GetKeyDown(KeyCode.T);

        isFClick = Input.GetKeyDown(KeyCode.F);

        is1 = Input.GetKeyDown(KeyCode.Alpha1);

        is2 = Input.GetKeyDown(KeyCode.Alpha2);

        is3 = Input.GetKeyDown(KeyCode.Alpha3);

        is4 = Input.GetKeyDown(KeyCode.Alpha4);

        is5 = Input.GetKeyDown(KeyCode.Alpha5);

        is6 = Input.GetKeyDown(KeyCode.Alpha6);
    }

    private IEnumerator WaitFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
    private IEnumerator Wait(float second, Action action)
    {
        yield return new WaitForSeconds(second);
        action?.Invoke();
    }
}
