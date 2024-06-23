using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> _inventorySlots;
    public List<InventorySlot> InventorySlots { get => _inventorySlots; set => _inventorySlots = value; }
    public int InventorySize => InventorySlots.Count;
    
    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }
    
    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        //if the item is already in the inventory, add the amount to the stack
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) 
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        //if dont have any item in the inventory or this slot have the same item is full stack, add the item to the first free slot
        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
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
}
