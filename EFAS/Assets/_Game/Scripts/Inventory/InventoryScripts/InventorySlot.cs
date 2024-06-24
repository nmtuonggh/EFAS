using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private int _stackSize;
    
    public InventoryItemData ItemData { get => _itemData; set => _itemData = value; }
    public int StackSize { get => _stackSize; set => _stackSize = value; }

    public InventorySlot(InventoryItemData source, int amount)
    {
        _itemData = source;
        _stackSize = amount;
    }

    public InventorySlot()
    {
        ClearData();
    }

    public void ClearData()
    {
        _itemData = null;
        _stackSize = -1;
    }
    
    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        _itemData = data;
        _stackSize = amount;
    }
    
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = _itemData.MaxStackItem - _stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (_stackSize + amountToAdd <= _itemData.MaxStackItem) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }
    
    
}
