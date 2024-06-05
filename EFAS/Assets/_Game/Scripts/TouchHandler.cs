using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 dragStartPosition;
    private bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPosition = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
}