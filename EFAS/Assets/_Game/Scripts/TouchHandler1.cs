using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private bool isTouching = false;
    private Vector2 previousTouchPosition;
    [SerializeField] private int activeFingerId = -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isTouching) return;
        isTouching = true;
        previousTouchPosition = eventData.position;
        
        foreach (var touch in Input.touches)
        {
            if (Vector2.Distance(touch.position, previousTouchPosition) < 10f)
            {
                activeFingerId = touch.fingerId;
                break;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 0)
        {
            ResetTouchState();
            return;
        }
        
        if (isTouching && activeFingerId != -1)
        {
            Touch currentTouch = GetTouchById(activeFingerId);
            if (currentTouch.fingerId != -1)
            {
                var currentTouchPosition = currentTouch.position;
                Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;
                CameraController.Instance.CameraRotation(deltaPosition);
                previousTouchPosition = currentTouchPosition;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Nếu ngón tay đang chạm được nhấc lên, thiết lập lại trạng thái
        if (eventData.pointerId == activeFingerId)
        {
            ResetTouchState();
        }
    }

    private void ResetTouchState()
    {
        isTouching = false;
        activeFingerId = -1;
    }

    private Touch GetTouchById(int fingerId)
    {
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == fingerId)
            {
                return touch;
            }
        }
        return new Touch();
    }
}
