using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class AirPlaneMovement : MonoBehaviour
{
    [SerializeField] private float steerSmoothSpeed = 1.5f;

    [Header("Physics")]
    [SerializeField] private float thrust = 60;
    [SerializeField] private float pitchTorque = 30;
    [SerializeField] private float yawTorque = 15;
    private Rigidbody rb;
    private Vector2 framInput;
    private Vector2 steerInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateSteering();
    }

    private void UpdateSteering()
    {
        steerInput = Vector2.Lerp(steerInput, framInput, Time.fixedDeltaTime * steerSmoothSpeed);
        var torque = new Vector3(steerInput.x * pitchTorque, steerInput.y * yawTorque, 0);

        rb.AddRelativeTorque(torque);
    }

    private void UpdateMovement()
    {
        var moveForce = transform.forward * thrust;
        rb.AddForce(moveForce);
    }

    public void SetSteerInput(Vector2 newFrameInput)
    {
        framInput = newFrameInput;
    }
}
