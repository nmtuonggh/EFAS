using System;
using System.Collections.Generic;
using _Game.Scripts.Movement.FiniteStateMachine.MovingLayer.Grounded;
using UnityEngine;
using Random = System.Random;


public class FishingRate : MonoBehaviour
{
    [SerializeField] private List<ItemData> _fishingItem;
    [SerializeField] private InventoryHolder _inventoryHolder;
    public event Action<ItemData> OnPopup;

    private void OnEnable()
    {
        FishingState.OnFishingEnd += OnFishingEnd;
    }

    private void OnFishingEnd()
    {
        var randomIndex = UnityEngine.Random.Range(0, _fishingItem.Count);
        for (var i = 0; i < _fishingItem.Count; i++)
        {
            if (i == randomIndex)
            {
                _inventoryHolder.InventorySystem.AddToInventory(_fishingItem[i], 1);
                OnPopup?.Invoke(_fishingItem[i]);
            }
        }
    }
}