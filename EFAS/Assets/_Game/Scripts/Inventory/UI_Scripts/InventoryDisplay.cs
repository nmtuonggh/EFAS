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
        //slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
    }

    public virtual void UpdateSlot(InventorySlot updateSlot)
    {
        
        foreach (var slot in slotDictionary)
        {
            //Debug.Log("input Slot" + updateSlot + "Slot in Dictionary" + slot.Value);
            if (slot.Value == updateSlot)   // if the slot is the same as the one i want to update
            {
                Debug.Log("Updating Slot");
                slot.Key.UpdateUISlot(updateSlot);
            }
        }
    }

    public abstract void SlotClicked(InventorySlot_UI clickedUISlot);
    
}
