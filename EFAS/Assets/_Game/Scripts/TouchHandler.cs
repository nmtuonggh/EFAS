using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isTouching = false;
    private Vector2 previousTouchPosition;
    public Transform cameraFollow;
    float rotationSpeed = 0.1f;
    
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
            Vector2 currentTouchPosition = eventData.position;
            Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;
            Debug.Log(previousTouchPosition + " " + currentTouchPosition);
            RotateCamera(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
    }
    
    private void RotateCamera(Vector2 deltaPosition)
    {
        float deltaX = deltaPosition.x * rotationSpeed;
        float deltaY = deltaPosition.y * rotationSpeed;
        
        cameraFollow.Rotate(Vector3.right, deltaX, Space.World);
        cameraFollow.Rotate(Vector3.up, -deltaY, Space.World);
        //cameraFollow.transform.rotation = Quaternion.Euler(deltaX, - deltaY, 0);
    }
}
