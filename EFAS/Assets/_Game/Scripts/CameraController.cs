using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    [Header("Camera Follow Object")] 
    [SerializeField]
    private Transform _cameraFollow;

    
    [Header("Values")] 
    [SerializeField] private float _cinemachineTargetYaw;
    [SerializeField] private float _cinemachineTargetPitch;
    [SerializeField] private float _sens = 2f;

    [Header("Clamp Values")] 
    private float _topClamp = 70.0f;
    private float _bottomClamp = -30.0f;

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
        _cinemachineTargetYaw = _cameraFollow.transform.rotation.eulerAngles.y;
    }

    public void CameraRotation(Vector2 outputPosition)
    {
        float deltaTimeMultiplier = Time.deltaTime * _sens;

        _cinemachineTargetYaw += outputPosition.x * deltaTimeMultiplier;
        _cinemachineTargetPitch += outputPosition.y * deltaTimeMultiplier;

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

        _cameraFollow.transform.rotation = Quaternion.Euler(-_cinemachineTargetPitch,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}