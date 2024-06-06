using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float topClamp = 70.0f;
    public float bottomClamp = -30.0f;
    
    private bool isTouching = false;
    private Vector2 previousTouchPosition;
    public Transform cameraFollow;
    
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        previousTouchPosition = eventData.position;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (isTouching)
        {
            var currentTouchPosition = eventData.position;
            Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;
            
            CameraRotation(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
    }
    

   
    private void CameraRotation(Vector2 outputPosition)
    {
        float deltaTimeMultiplier = Time.deltaTime;

        cinemachineTargetYaw += outputPosition.x * deltaTimeMultiplier;
        cinemachineTargetPitch += outputPosition.y * deltaTimeMultiplier;
        
        // clamp our rotations so our values are limited 360 degrees
        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

        // Cinemachine will follow this target
        cameraFollow.transform.rotation = Quaternion.Euler(cinemachineTargetPitch,
            cinemachineTargetYaw, 0.0f);
    }
    
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
