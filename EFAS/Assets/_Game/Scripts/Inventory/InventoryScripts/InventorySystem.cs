using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using _Game.Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem 
{
    [SerializeField] private List<ItemData> allItemData;
    [SerializeField] private List<InventorySlot> _inventorySlots;
    public List<InventorySlot> InventorySlots { get => _inventorySlots; set => _inventorySlots = value; }
    public int InventorySize => InventorySlots.Count;

    public List<ItemData> AllItemData
    {
        get => allItemData;
        set => allItemData = value;
    }
    
    private GameEventT<InventorySlot> onInventorySlotChangedEventT;

    public InventorySystem(int size, GameEventT<InventorySlot> onInventorySlotChangedEventT)
    {
        _inventorySlots = new List<InventorySlot>(size);
        this.onInventorySlotChangedEventT = onInventorySlotChangedEventT;
        for (int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }
    
    public bool AddToInventory(ItemData itemToAdd, int amountToAdd)
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
                        onInventorySlotChangedEventT.Raise(slot);
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
                onInventorySlotChangedEventT.Raise(freeSlot);
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
    
    public bool RemoveFromInventory(InventorySlot slotToRemove, ItemData itemToRemove, int amountToRemove)
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
                onInventorySlotChangedEventT.Raise(slotToRemove);
                return true;
            }
        }
        return false;
    }
    
    public bool ContainsItem(ItemData itemToAdd, out List<InventorySlot> invSlot)
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
        onInventorySlotChangedEventT.Raise(slotToDrop);
    }
    
}

