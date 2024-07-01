using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] protected InventorySystem _inventorySystem;
    public GameEvent<InventorySlot> OnInventorySlotChangedEvent;

    public InventorySystem InventorySystem => _inventorySystem;
    
    private void Awake()
    {
        _inventorySystem = new InventorySystem(_inventorySize, OnInventorySlotChangedEvent);
    }
}
