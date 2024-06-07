using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private bool isTouching = false;
    private Vector2 previousTouchPosition;
    [SerializeField] private int activeFingerId = -1;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("On Start PDown =>" + "isT : " + isTouching + "|| id : " + activeFingerId);

        if (!isTouching || activeFingerId == -1)
        {
            isTouching = true;
            previousTouchPosition = eventData.position;
            activeFingerId = eventData.pointerId;
        }
        //Debug.Log("On End PDown =>" + "isT : " + isTouching + "|| id : " + activeFingerId);

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnStartPoiDrag => isT : " + isTouching + "|| id : " + activeFingerId +"|| evenID: "+ eventData.pointerId);

        if (isTouching && eventData.pointerId == activeFingerId)
        {
            var currentTouchPosition = eventData.position;
            Vector2 deltaPosition = currentTouchPosition - previousTouchPosition;

            CameraController.Instance.CameraRotation(deltaPosition);
            previousTouchPosition = currentTouchPosition;
        }
        //Debug.Log("OnEndPoiDrag => isT : " + isTouching + "|| id : " + activeFingerId +"|| evenID: "+ eventData.pointerId);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnStart PUp => isT : " + isTouching + "| id : " + activeFingerId +"| evenID: "+ eventData.pointerId);

        if (eventData.pointerId == activeFingerId)
        {
            //reset id when 1st finger up
            activeFingerId = -1;
            
            // check other finger
            if (Input.touchCount > 0)
            {
                //Debug.Log("|touch count : " + Input.touchCount);

                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        //Debug.Log("Touch index = " + i);
                        //activeFingerId = touch.fingerId -1;
                        activeFingerId = touch.fingerId;
                        //Debug.Log("|fingerid : " + touch.fingerId);

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
        //Debug.Log("OnEndPUp => isT : " + isTouching + "| id : " + activeFingerId +"| evenID: "+ eventData.pointerId);
    }
}
