using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.EventSystems;

public class FixedTouching : MonoBehaviour, IDragHandler , IPointerUpHandler , IPointerDownHandler
{
    [SerializeField] private Image imgCamControl;
    [SerializeField] private CinemachineFreeLook cameraFreeLook;
    private string strMouseX = "Touch X", strMouseY = "Mouse Y";

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                imgCamControl.rectTransform,
                eventData.position,
                eventData.enterEventCamera,
                out Vector2 posOut))
        {
            Debug.Log(posOut);
            cameraFreeLook.m_XAxis.m_InputAxisName = strMouseX;
            cameraFreeLook.m_YAxis.m_InputAxisName = strMouseY;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cameraFreeLook.m_XAxis.m_InputAxisName = null;
        cameraFreeLook.m_YAxis.m_InputAxisName = null;

        cameraFreeLook.m_XAxis.m_InputAxisValue = 0;
        cameraFreeLook.m_YAxis.m_InputAxisValue = 0;
    }
}