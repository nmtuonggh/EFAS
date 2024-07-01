using _Game.Scripts.Event;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener<T> 
{
    [Tooltip("Event to register with.")] public GameEvent<T> Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<T> Response;

    public void OnEnable()
    {
        Event.RegisterListener(this);
    }

    public void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T t)
    {
        Response.Invoke(t);
    }
}