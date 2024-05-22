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
        mouseDeltaX = Input.GetAxis("Mouse X");

        mouseDeltaY = Input.GetAxis("Mouse Y");

        moveHorizontal = joystick.Horizontal;

        moveVertical = joystick.Vertical;
    }

    private void SubscribeMobileInput()
    {
        spaceButton.OnHandDown += () => space = true;
        spaceButton.OnHandUp += () => space = false;

        isEButton.OnHandDown += () => isE = true;
        isEButton.OnHandUp += () => isE = false;

        isTabButton.OnHandDown += () => isTab = true;
        isTabButton.OnHandUp += () => isTab = false;

        isTButton.OnHandDown += () => isT = true;
        isTButton.OnHandUp += () => isT = false;

        isT2Button.OnHandDown += () => isT = true;
        isT2Button.OnHandUp += () => isT = false;

        isFButton.onClick.AddListener(() =>
        {
            isFClick = true;
            StartCoroutine(WaitFrame(() => isFClick = false));
        });

        is1Button.OnHandDown += () => is1 = true;
        is1Button.OnHandUp += () => is1 = false;

        is2Button.OnHandDown += () => is2 = true;
        is2Button.OnHandUp += () => is2 = false;

        is3Button.OnHandDown += () => is3 = true;
        is3Button.OnHandUp += () => is3 = false;

        is4Button.OnHandDown += () => is4 = true;
        is4Button.OnHandUp += () => is4 = false;

        is5Button.OnHandDown += () => is5 = true;
        is5Button.OnHandUp += () => is5 = false;
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
    }

    private IEnumerator WaitFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}
