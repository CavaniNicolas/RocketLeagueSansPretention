using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // --- Référence au système d'input ---
    [Header("Input Reference")]
    public PlayerInputController inputController; // À assigner dans l'inspecteur ou via Awake

    // --- Physics Settings ---
    [Header("Car Settings")]
    public float motorForce = 100f;
    public float brakeForce = 1000f;
    public float maxSteerAngle = 30f;

    // --- Wheel Colliders ---
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    // --- Wheel Visual Transforms ---
    [Header("Wheel Transforms")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    // --- Private Variables ---
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking;

    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Traduit le New Input System vers vos variables locales
    private void GetInput()
    {
        if (inputController == null) return;

        // Récupère l'axe X (gauche/droite) et l'axe Y (avant/arrière) du Vector2
        horizontalInput = inputController.myMovementInputVector.x;
        verticalInput = inputController.myMovementInputVector.y;
        
        // Récupère l'état du frein
        isBraking = inputController.isBrakingInput;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}