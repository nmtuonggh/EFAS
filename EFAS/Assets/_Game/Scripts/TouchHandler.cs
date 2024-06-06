using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isTouching = false;
    private Vector2 previousTouchPosition;
    
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    
    [SerializeField] PlayerController playerController;
    
    
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
            
            playerController.CameraRotation(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
    }
    
}
