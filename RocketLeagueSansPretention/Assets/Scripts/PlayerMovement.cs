using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Birdo3DMovement : MonoBehaviour
{
    [SerializeField]
    private float myMoveSpeed = 50;
    [SerializeField]
    private float myJumpSpeed = 10;

    private PlayerInputController myPlayerInputController;
    private Rigidbody myRigidbody;
    private bool myIsJumpTriggered;

    private void Awake()
    {
        myPlayerInputController = GetComponent<PlayerInputController>();
        myRigidbody = GetComponent<Rigidbody>();
        myPlayerInputController.myOnJumpButtonPressed += OnJumpoButtonPressed;
    }

    public void FixedUpdate()
    {        
        Vector3 velocity = new Vector3(
        myPlayerInputController.myMovementInputVector.x,
        0,
        myPlayerInputController.myMovementInputVector.y)
        * myMoveSpeed;

        velocity.y = myRigidbody.linearVelocity.y;

        if (myIsJumpTriggered)
        {
            velocity.y = myJumpSpeed;
            myIsJumpTriggered = false;
        }

        myRigidbody.linearVelocity = velocity;
    }

    private void OnJumpoButtonPressed()
    { 
        myIsJumpTriggered = true;
    }
}
