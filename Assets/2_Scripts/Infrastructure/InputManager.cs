using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float mouseDeltaX;
    private float mouseDeltaY;
    private float moveHorizontal;
    private float moveVertical;
    private bool space;
    private bool IsE;
    private bool IsT;
    private bool IsTab;
    private bool IsF;
    private bool Is1;
    private bool Is2;
    private bool Is3;
    public float GetMouseDeltaX => mouseDeltaX;
    public float GetMouseDeltaY => mouseDeltaY;
    public float GetMoveHorizontal => moveHorizontal;
    public float GetMoveVertical => moveVertical;
    public bool GetSpace => space;
    public bool GetIsE => IsE;
    public bool GetIsTab => IsTab;
    public bool GetIsT => IsT;
    public bool GetIsF => IsF;
    public bool GetIs1 => Is1;
    public bool GetIs2 => Is2;
    public bool GetIs3 => Is3;

    void Update()
    {
        mouseDeltaX = Input.GetAxis("Mouse X");

        mouseDeltaY = Input.GetAxis("Mouse Y");

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        space = Input.GetButtonDown("Jump");

        IsE = Input.GetKey(KeyCode.E);

        IsTab = Input.GetKeyDown(KeyCode.Tab);

        IsT = Input.GetKeyDown(KeyCode.T);

        IsF = Input.GetKeyDown(KeyCode.F);

        Is1 = Input.GetKeyDown(KeyCode.Alpha1);

        Is2 = Input.GetKeyDown(KeyCode.Alpha2);

        Is3 = Input.GetKeyDown(KeyCode.Alpha3);
    }
}
