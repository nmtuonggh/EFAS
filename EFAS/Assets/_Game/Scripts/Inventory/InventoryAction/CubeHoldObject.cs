using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class CubeHoldObject : MonoBehaviour
    {
        [SerializeField] Transform[] _cubeHold;
        public GameEventListener OnHoldState;
        public GameEventListener UnHoldState;

        private void Awake()
        {
            OnHoldState.OnEnable();
            UnHoldState.OnEnable();
        }
        
        private void OnDestroy()
        {
            OnHoldState.OnDisable();
            UnHoldState.OnDisable();
        }

        public void OnHold()
        {
            foreach (var cube in _cubeHold)
            {
                cube.gameObject.SetActive(true);
            }
        }

        public void OnDrop()
        {
            foreach (var cube in _cubeHold)
            {
                cube.gameObject.SetActive(false);
            }
        }
    }
}