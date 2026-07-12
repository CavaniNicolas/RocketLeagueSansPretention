using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 myMovementInputVector { get; private set; }
    public bool isBrakingInput { get; private set; } // <-- AJOUTÉ : Pour le frein de la voiture
    public event Action myOnJumpButtonPressed; // CallBack set in PlayerMovement
    public event Action<bool> myOnCameraRollPressedOrReleased; // CallBack set in PlayerCamera
    public Vector2 myLookInputVector { get; private set; }

    private PlayerInput myPlayerInput;

    // Cette méthode gère le mouvement à pied ET en voiture si l'action s'appelle "Move" dans les deux cartes
    private void OnMove(InputValue anInputValue)
    {
        Debug.Log(anInputValue.Get<Vector2>());
        myMovementInputVector = anInputValue.Get<Vector2>();
    }

    // Appelé par l'action "Brake" de votre Action Map "Car"
    private void OnBrake(InputValue anInputValue)
    {
        isBrakingInput = anInputValue.isPressed;
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

    void Awake()
    {
        myPlayerInput = GetComponent<PlayerInput>();
    }

    public void EnterVehicle()
    {
        myPlayerInput.SwitchCurrentActionMap("Car");
    }

    public void ExitVehicle()
    {
        myPlayerInput.SwitchCurrentActionMap("Player");
    }
}
