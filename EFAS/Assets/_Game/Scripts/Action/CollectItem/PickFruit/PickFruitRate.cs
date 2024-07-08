using System;
using System.Collections.Generic;
using _Game.Scripts.Movement.FiniteStateMachine.MovingLayer.Grounded;
using UnityEngine;

namespace _Game.Scripts.Action.PickFruit
{
    public class PickFruitRate : MonoBehaviour
    {
        [SerializeField] private List<ItemData> _fruitItem;
        [SerializeField] private InventoryHolder _inventoryHolder;
        public event Action<ItemData> OnPopupPickFruit;

        private void OnEnable()
        {
            PickFruitState.OnPickFruitEnd += OnPickFruitEnd;
        }

        private void OnPickFruitEnd()
        {
            var randomIndex = UnityEngine.Random.Range(0, _fruitItem.Count);
            for (var i = 0; i < _fruitItem.Count; i++)
            {
                if (i == randomIndex)
                {
                    _inventoryHolder.InventorySystem.AddToInventory(_fruitItem[i], 3);
                    OnPopupPickFruit?.Invoke(_fruitItem[i]);
                    Debug.Log("Fishing item" + _fruitItem[i].DisplayName + " added to inventory");
                }
            }
        }
    }
}