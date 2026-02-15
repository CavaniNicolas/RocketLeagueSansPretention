using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private PlayerInputController myPlayerInputController;
    private Camera myCamera;

    [SerializeField]
    private float myMouseSensitivity = 0.1f;
    [SerializeField]
    private bool myInvertCamera = false;
    // bonjourdsjjsdjds
    [SerializeField]
    private Vector2 myPitchClamp = new Vector2(-40.0f, 50f);
    [SerializeField]
    private Vector2 myRollClamp = new Vector2(-30.0f, 30f);
    [SerializeField]
    float myRollResetSpeed = 4.0f;

    private bool myIsRollActivated = false;
    private float myPitch = 0.0f;
    private float myYaw = 0.0f;
    [SerializeField]
    private float myRoll = 0.0f;

    private void Awake()
    {
        myPlayerInputController = GetComponent<PlayerInputController>();
        myCamera = GetComponentInChildren<Camera>();
        myPlayerInputController.myOnCameraRollPressedOrReleased += OnCameraRollPressedOrReleased;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void FixedUpdate()
    {
        Vector2 delta = myPlayerInputController.myLookInputVector * myMouseSensitivity;

        //gameObject.transform.Rotate(Vector3.up * delta.x * myMouseSensitivity);

        //myCamera.transform.Rotate(Vector3.left * delta.y * myMouseSensitivity);
        //myCamera.transform.eulerAngles.Set(Mathf.Clamp(myCamera.transform.eulerAngles.x, 90, -90), myCamera.transform.eulerAngles.y, myCamera.transform.eulerAngles.z);

        int invertCamera = 1;
        if (myInvertCamera)
        {
            invertCamera = -1;
        }

        if (myIsRollActivated)
        {
            myRoll -= delta.x;
            myRoll = Mathf.Clamp(myRoll, myRollClamp.x, myRollClamp.y);
        }
        else
        {
            myYaw += delta.x;
            myYaw = Mathf.Repeat(myYaw, 360f); // Keep between 0 and 360 if you want.
            myRoll = Mathf.Lerp(myRoll, 0, Time.deltaTime * myRollResetSpeed); // improve Lerp so it is linear using start-caluculation time / the time we want it to take
            if (Mathf.Abs(myRoll) < 0.1)
                myRoll = 0;
        }

        myPitch -= delta.y * invertCamera;
        myPitch = Mathf.Clamp(myPitch, myPitchClamp.x, myPitchClamp.y);

        myCamera.transform.eulerAngles = new Vector3(myPitch, gameObject.transform.eulerAngles.y, myRoll);
        gameObject.transform.eulerAngles = new Vector3(0, myYaw, 0);
    }

    private void OnCameraRollPressedOrReleased(bool anIsPressed)
    {
        myIsRollActivated = anIsPressed;
    }
}
