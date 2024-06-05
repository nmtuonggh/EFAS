using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    
    public void PanCamera(float deltaX, float deltaY)
    {
        freeLookCamera.m_XAxis.Value += deltaX;
        freeLookCamera.m_YAxis.Value += deltaY;
    }
}
