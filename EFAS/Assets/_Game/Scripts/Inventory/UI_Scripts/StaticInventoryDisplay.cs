using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory.Action;
using DG.Tweening;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    [SerializeField] private DropItem _dropItem;
    [SerializeField] private HoldeItem _holdeItem;
    public event Action OnFocusSlotTouch;
    

    protected override void Start()
    {
        base.Start();
        if (_inventoryHolder != null)
        {
            _inventorySystem = _inventoryHolder.InventorySystem;
            _inventorySystem.OnInventorySlotChanged += UpdateSlot;
            _dropItem.OnDropItemUpdate += UpdateSlot;
            _holdeItem.OnHoldeItemSlotChangedEvent += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No Inventory Assigned to  {this.gameObject}");
        }
        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
        
        if(slots.Length != _inventorySystem.InventorySize)
        {
            Debug.LogWarning("Slot Dictionary and Inventory Size Mismatch");
            return;
        }
        
        for(int i =0 ; i < _inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], _inventorySystem.InventorySlots[i]);
            slots[i].Init(_inventorySystem.InventorySlots[i]);
        }
    }

    public override void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        SetFocus(clickedUISlot);
    }
    
    private void SetFocus(InventorySlot_UI clickedUISlot)
    {
        if (FocusSlot != null)
        {
            FocusSlot.FocusLine.SetActive(false);
        }

        if (clickedUISlot.AssingnedInventorySlot.ItemData != null)
        {
            FocusSlot = clickedUISlot;
            FocusSlot.FocusLine.SetActive(true);
            //OnFocusSlotTouch?.Invoke();
        }
    }
}
