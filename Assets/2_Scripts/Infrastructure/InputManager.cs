using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float mouseDeltaX;
    private float mouseDeltaY;
    private float moveHorizontal;
    private float moveVertical;
    private bool space;
    private bool isE;
    private bool isT;
    private bool isTab;
    private bool isF;
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
    public bool GetIsF => isF;
    public bool GetIs1 => is1;
    public bool GetIs2 => is2;
    public bool GetIs3 => is3;
    public bool GetIs4 => is4;
    public bool GetIs5 => is5;

    void Update()
    {
        mouseDeltaX = Input.GetAxis("Mouse X");

        mouseDeltaY = Input.GetAxis("Mouse Y");

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        space = Input.GetButtonDown("Jump");

        isE = Input.GetKey(KeyCode.E);

        isTab = Input.GetKeyDown(KeyCode.Tab);

        isT = Input.GetKeyDown(KeyCode.T);

        isF = Input.GetKeyDown(KeyCode.F);

        is1 = Input.GetKeyDown(KeyCode.Alpha1);

        is2 = Input.GetKeyDown(KeyCode.Alpha2);

        is3 = Input.GetKeyDown(KeyCode.Alpha3);

        is4 = Input.GetKeyDown(KeyCode.Alpha4);

        is5 = Input.GetKeyDown(KeyCode.Alpha5);
    }
}
