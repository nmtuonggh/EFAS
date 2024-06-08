using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TouchHandler1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private bool _isTouching = false;
    private Vector2 _previousTouchPosition;
    [SerializeField] private int _activeFingerId = -1;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isTouching) return;
        _isTouching = true;
        _previousTouchPosition = eventData.position;
        
        foreach (var touch in Input.touches)
        {
            if (Vector2.Distance(touch.position, _previousTouchPosition) < 10f)
            {
                _activeFingerId = touch.fingerId;
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
        
        if (_isTouching && _activeFingerId != -1)
        {
            Touch currentTouch = GetTouchById(_activeFingerId);
            if (currentTouch.fingerId != -1)
            {
                var currentTouchPosition = currentTouch.position;
                Vector2 deltaPosition = currentTouchPosition - _previousTouchPosition;
                CameraController.Instance.CameraRotation(deltaPosition);
                _previousTouchPosition = currentTouchPosition;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Nếu ngón tay đang chạm được nhấc lên, thiết lập lại trạng thái
        if (eventData.pointerId == _activeFingerId)
        {
            ResetTouchState();
        }
    }

    private void ResetTouchState()
    {
        _isTouching = false;
        _activeFingerId = -1;
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
