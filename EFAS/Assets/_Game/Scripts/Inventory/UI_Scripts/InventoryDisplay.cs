using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    protected InventorySystem _inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    protected InventorySlot_UI _focusSlot;

    public InventorySystem InventorySystem1 => _inventorySystem;
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

    protected virtual void UpdateSlot(InventorySlot updateSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updateSlot)   // if the slot is the same as the one i want to update
            {
                slot.Key.UpdateUISlot(updateSlot);
            }
        }
    }
    
    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        Debug.Log("slot clicked");
        SetFocus(clickedUISlot);

        if (FocusSlot != null && FocusSlot.AssingnedInventorySlot.ItemData != null)
        {
            _inventorySystem.DropOneItem(FocusSlot.AssingnedInventorySlot);
            //spawnWorldItem.SpawnItem(_focusSlot.AssingnedInventorySlot.ItemData);
        }
    }

    private void SetFocus(InventorySlot_UI clickedUISlot)
    {
        if (FocusSlot != null)
        {
            FocusSlot.FocusLine.SetActive(false);
        }

        FocusSlot = clickedUISlot;
        FocusSlot.FocusLine.SetActive(true);
    }
}
