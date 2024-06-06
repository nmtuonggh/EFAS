using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isTouching = false;
    private Vector2 previousTouchPosition;
    private int activeFingerId = -1;

    [SerializeField] PlayerController playerController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isTouching || activeFingerId == -1)
        {
            isTouching = true;
            previousTouchPosition = eventData.position;
            activeFingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == activeFingerId)
        {
            //reset id when 1st finger up
            activeFingerId = -1;

            // check other finger
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

    public void OnDrag(PointerEventData eventData)
    {
        if (isTouching && eventData.pointerId == activeFingerId)
        {
            var currentTouchPosition = eventData.position;
            Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;

            playerController.CameraRotation(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
    }

    public void checkTouch()
    {
        Debug.Log(Input.touchCount);
        for (int i = 0; i < Input.touchCount; i++)
        {
            Debug.Log("touch id: " + Input.GetTouch(i).fingerId);
        }
    }
}
