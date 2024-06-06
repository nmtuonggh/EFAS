using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isTouching = false;
    private Vector2 previousTouchPosition;
    
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    private int activefingerID = -1;
    
    [SerializeField] PlayerController playerController;
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        previousTouchPosition = eventData.position;
        activefingerID = eventData.pointerId;
        Debug.Log("evenpointer id:" + eventData.pointerId);
        //checkTouch();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isTouching && activefingerID == eventData.pointerId)
        {
            isTouching = false;
            activefingerID = -1;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On drag: activeF"+ activefingerID + " evedata" + eventData.pointerId );
        if (isTouching && activefingerID == eventData.pointerId)
        {
            var currentTouchPosition = eventData.position;
            Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;
            
            playerController.CameraRotation(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
    }
    
    public void checkTouch()
    {
        Touch touch = Input.GetTouch(0);
        Debug.Log(Input.touchCount);
        for(int i=0; i < Input.touchCount; i++)
        {
            Debug.Log("touch id:" + Input.GetTouch(i).fingerId);
        }
        //Debug.Log("touch id:" + touch.fingerId);
    }
    
}
