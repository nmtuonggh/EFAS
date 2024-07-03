using System;
using System.Collections.Generic;
using _Game.Scripts.Shop;
using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    public ShopInvenHolder ShopInvenHolder;
    public ShopSlot_UI[] slots;
    public ShopSlot_UI _focusSlot;
    public Dictionary<ShopSlot_UI, ShopInvenSlot> slotsDictionary;
    public List<ItemData> _shopItems;

    public ShopSlot_UI FocusSlot
    {
        get => _focusSlot;
        set => _focusSlot = value;
    }

    private void Start()
    {
        AssignSlot(ShopInvenHolder.InvenSystem);
        AssignDataToSlots(); 

        foreach (var slot in slotsDictionary)
        {
            slot.Key.UpdateShopUISlot(slot.Value);
        }
    }

    public void AssignSlot(ShopInvenSystem invenSystem)
    {
        slotsDictionary = new Dictionary<ShopSlot_UI, ShopInvenSlot>();

        if (slots.Length != invenSystem.ShopInvenSize)
        {
            Debug.LogError("Slot length does not match the inventory size");
            return;
        }

        for (int i = 0; i < invenSystem.ShopInvenSize; i++)
        {
            slotsDictionary.Add(slots[i], invenSystem.ShopInvenSlots[i]);
            slots[i].Init(invenSystem.ShopInvenSlots[i]);
        }
    }

    public void AssignDataToSlots() 
    {
        if (_shopItems.Count != slotsDictionary.Count)
        {
            Debug.LogError("The number of items does not match the number of slots");
            return;
        }

        for (int i = 0; i < _shopItems.Count; i++)
        {
            ShopInvenSlot slot = slotsDictionary[slots[i]];
            slot.Data = _shopItems[i];
            slots[i].UpdateShopUISlot(slot);
        }
    }

    public void ShopSlotClicked(ShopSlot_UI clickedUISlot)
    {
        SetFocus(clickedUISlot);
    }

    private void SetFocus(ShopSlot_UI clickedUISlot)
    {
        if (_focusSlot != null)
        {
            _focusSlot.FocusLine.SetActive(false);
        }
        
        if (clickedUISlot.AssingnedInventorySlot.Data != null)
        {
            _focusSlot = clickedUISlot;
            _focusSlot.FocusLine.SetActive(true);
        }
    }
}