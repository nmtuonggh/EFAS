using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem 
{
    [SerializeField] private List<InventoryItemData> allItemData;
    [SerializeField] private List<InventorySlot> _inventorySlots;
    public List<InventorySlot> InventorySlots { get => _inventorySlots; set => _inventorySlots = value; }
    public int InventorySize => InventorySlots.Count;

    public List<InventoryItemData> AllItemData
    {
        get => allItemData;
        set => allItemData = value;
    }
    
    private GameEvent<InventorySlot> _onInventorySlotChangedEvent;

    public InventorySystem(int size, GameEvent<InventorySlot> OnInventorySlotChangedEvent)
    {
        _inventorySlots = new List<InventorySlot>(size);
        this._onInventorySlotChangedEvent = OnInventorySlotChangedEvent;
        for (int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }
    
    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        while (amountToAdd > 0)
        {
            if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
            {
                foreach (var slot in invSlot)
                {
                    if (slot.RoomLeftInStack(amountToAdd, out int amountRemaining))
                    {
                        int amountToAddToSlot = Math.Min(amountToAdd, amountRemaining);
                        slot.AddToStack(amountToAddToSlot);
                        amountToAdd -= amountToAddToSlot;
                        //OnInventorySlotChanged?.Invoke(slot);
                        _onInventorySlotChangedEvent.Raise(slot);
                        if (amountToAdd == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            if (HasFreeSlot(out InventorySlot freeSlot))
            {
                int amountToAddToSlot = Math.Min(amountToAdd, itemToAdd.MaxStackItem);
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAddToSlot);
                amountToAdd -= amountToAddToSlot;
                //OnInventorySlotChanged?.Invoke(freeSlot);
                _onInventorySlotChangedEvent.Raise(freeSlot);
                if (amountToAdd == 0)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
    public bool RemoveFromInventory(InventorySlot slotToRemove, InventoryItemData itemToRemove, int amountToRemove)
    {
        if (slotToRemove.ItemData == itemToRemove)
        {
            if (slotToRemove.StackSize >= amountToRemove)
            {
                slotToRemove.RemoveFromStack(amountToRemove);
                if (slotToRemove.StackSize == 0)
                {
                    slotToRemove.ClearData();
                }
                //OnInventorySlotChanged?.Invoke(slotToRemove);
                _onInventorySlotChangedEvent.Raise(slotToRemove);
                return true;
            }
        }
        return false;
    }
    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    { //if in the inventory have the same item, return true and get all the slots that have the item then return to a list :)
        invSlot = InventorySlots.Where(currentSlot => currentSlot.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true;
    }
    
    public bool HasFreeSlot(out InventorySlot freeSlot) //if there is a free slot in the inventory, return true
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ItemData == null);
        return freeSlot == null ? false : true;
    }
    
    public void DropOneItem(InventorySlot slotToDrop)
    {
        slotToDrop.RemoveFromStack(1);
        //OnInventorySlotChanged?.Invoke(slotToDrop);
        _onInventorySlotChangedEvent.Raise(slotToDrop);
    }
    
}

