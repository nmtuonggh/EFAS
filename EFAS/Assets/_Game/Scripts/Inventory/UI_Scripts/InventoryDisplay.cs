using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    protected InventorySystem _inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    protected InventorySlot_UI _focusSlot;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    public InventorySlot_UI FocusSlot
    {
        get => _focusSlot;
        set => _focusSlot = value;
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);
    
    protected virtual void Start()  
    {
    }

    public virtual void UpdateSlot(InventorySlot updateSlot)
    {
        
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updateSlot)   // if the slot is the same as the one i want to update
            {
                slot.Key.UpdateUISlot(updateSlot);
            }
        }
    }

    public abstract void SlotClicked(InventorySlot_UI clickedUISlot);
    
}
