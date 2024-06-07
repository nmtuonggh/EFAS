using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Camera Follow Object")] 
    [SerializeField]
    Transform cameraFollow;

    [Header("Values")] 
    [SerializeField] private float cinemachineTargetYaw;
    [SerializeField] private float cinemachineTargetPitch;
    [SerializeField] private float sens = 2f;

    [Header("Clamp Values")] 
    public float topClamp = 70.0f;
    public float bottomClamp = -30.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cinemachineTargetYaw = cameraFollow.transform.rotation.eulerAngles.y;
    }

    public void CameraRotation(Vector2 outputPosition)
    {
        float deltaTimeMultiplier = Time.deltaTime * sens;

        cinemachineTargetYaw += outputPosition.x * deltaTimeMultiplier;
        cinemachineTargetPitch += outputPosition.y * deltaTimeMultiplier;

        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

        cameraFollow.transform.rotation = Quaternion.Euler(-cinemachineTargetPitch,
            cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}