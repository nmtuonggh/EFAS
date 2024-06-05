using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraControlHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image imgCamControl;
    [SerializeField] public float sensitivity = 50f;

    [SerializeField] private CinemachineFreeLook freeLookCamera;
    private bool isDragging = false;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
            float xAxisValue = eventData.delta.x * sensitivity * Time.deltaTime;
            float yAxisValue = eventData.delta.y * 0.1f * sensitivity * Time.deltaTime;

            freeLookCamera.m_YAxis.m_InputAxisValue += xAxisValue;
            freeLookCamera.m_YAxis.m_InputAxisValue += yAxisValue;
    }
}