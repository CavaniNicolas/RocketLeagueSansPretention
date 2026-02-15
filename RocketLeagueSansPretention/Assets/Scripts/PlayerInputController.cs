using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 myMovementInputVector { get; private set; }
    public event Action myOnJumpButtonPressed; // CallBack set in PlayerMovement
    public event Action<bool> myOnCameraRollPressedOrReleased; // CallBack set in PlayerCamera
    public Vector2 myLookInputVector { get; private set; }
    private void OnMove(InputValue anInputValue)
    {
        //Debug.Log(anInputValue.Get<Vector2>());
        myMovementInputVector = anInputValue.Get<Vector2>();
    }

    private void OnJump(InputValue anInputValue)
    {
        if (anInputValue.isPressed)
        {
            myOnJumpButtonPressed?.Invoke();
        }
    }

    private void OnLook(InputValue anInputValue)
    {
        // Debug.Log(anInputValue.Get<Vector2>());
        myLookInputVector = anInputValue.Get<Vector2>();
    }

    private void OnRollCamera(InputValue anInputValue)
    {
        myOnCameraRollPressedOrReleased?.Invoke(anInputValue.isPressed);
    }
}
