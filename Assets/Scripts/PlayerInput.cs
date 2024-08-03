using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Particle effects")]
    [SerializeField] private ParticleSystem snowballs;

    [Header("Input system")]
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction fire;

    [Header("Movement adjustment settings")]
    [SerializeField] private float xVelocity = 1;
    [SerializeField] private float yVelocity = 1;

    [SerializeField] private float clampRangeX = 10;
    [SerializeField] private float clampRangeY = 10;

    [SerializeField] private float pitchPosFactor = 10;
    [SerializeField] private float pitchControlFactor = 10;
    [SerializeField] private float yawPosFactor = 5;
    [SerializeField] private float rollControlFactor = 10;

    [SerializeField] private float maxRotationFactor = 0.5f;

    private float xIn;
    private float yIn;

    private void Start()
    {
        var emission = snowballs.emission;
        emission.enabled = true;
    }

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    private void Update()
    {

        xIn = movement.ReadValue<Vector2>().x;
        yIn = movement.ReadValue<Vector2>().y;

        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        float rawXOffset = xIn * xVelocity * Time.deltaTime;
        float rawYOffset = yIn * yVelocity * Time.deltaTime;
        float rawX = transform.localPosition.x + rawXOffset;
        float rawY = transform.localPosition.y + rawYOffset;
        float clampedX = Mathf.Clamp(rawX, -clampRangeX, clampRangeX);
        float clampedY = Mathf.Clamp(rawY, -clampRangeY, clampRangeY);

        transform.localPosition = new Vector3(
            clampedX,
            clampedY,
            transform.localPosition.z
            );
    }

    private void ProcessRotation()
    {
        float deltaPitchDueToPos = transform.localPosition.y * pitchPosFactor;
        float deltaPitchDueToControl = -yIn * pitchControlFactor;
        float deltaYawDueToPos = -transform.localPosition.x * yawPosFactor;
        float deltaRollDueToControl = -xIn * rollControlFactor;

        Quaternion targetRotation = Quaternion.Euler(deltaPitchDueToControl + deltaPitchDueToPos, deltaYawDueToPos, deltaRollDueToControl);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, maxRotationFactor * Time.deltaTime);
    }

    private void ProcessFiring()
    {
        var emission = snowballs.emission;
        if (fire.IsPressed())
            emission.enabled = true;
        else
            emission.enabled = false;
    }
}
