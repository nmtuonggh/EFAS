using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private bool isTouching = false;
    private Vector2 previousTouchPosition;
    [SerializeField] private int activeFingerId = -1;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isTouching || activeFingerId == -1)
        {
            isTouching = true;
            previousTouchPosition = eventData.position;
            activeFingerId = eventData.pointerId;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (isTouching && eventData.pointerId == activeFingerId)
        {
            var currentTouchPosition = eventData.position;
            Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;

            CameraController.Instance.CameraRotation(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == activeFingerId)
        {
            activeFingerId = -1;
            
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        activeFingerId = touch.fingerId;
                        previousTouchPosition = touch.position;
                        break;
                    }
                }
            }
            else
            {
                isTouching = false;
            }
        }
    }
}
