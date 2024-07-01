using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Event
{
    [Serializable]
    public class GameEvent<T> : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField] private List<GameEventListener<T>> eventListeners = 
            new List<GameEventListener<T>>();

        public void Raise(T t)
        {
            for(int i = eventListeners.Count -1; i >= 0; i--)
                eventListeners[i].OnEventRaised(t);
        }

        public void RegisterListener(GameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}