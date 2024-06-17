using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private InputAction _action;
    [SerializeField] private KeyCode _keyBind;

    public void OnPointerDown(PointerEventData eventData)
    {
        _action.Down = true;
        _action.Pressing = true;
        StartCoroutine(IE_ResetPointerDown(1));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _action.Up = true;
        _action.Pressing = false;
        StartCoroutine(IE_ResetPointerUp(1));
    }
    
    private IEnumerator IE_ResetPointerDown(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }
        _action.Down = false;
    }

    private IEnumerator IE_ResetPointerUp(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }

        _action.Up = false;
    }
}
