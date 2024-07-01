using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] protected InventorySystem _inventorySystem;
    [FormerlySerializedAs("OnInventorySlotChangedEvent")] public GameEventT<InventorySlot> onInventorySlotChangedEventT;

    public InventorySystem InventorySystem => _inventorySystem;
    
    private void Awake()
    {
        _inventorySystem = new InventorySystem(_inventorySize, onInventorySlotChangedEventT);
    }
}
