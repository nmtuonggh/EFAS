using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory.PickupInventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PickupItemSystem : MonoBehaviour
{
    [FormerlySerializedAs("_itemsInRange")] [SerializeField] private List<ItemPickedUp> _listItemsInRange = new List<ItemPickedUp>();
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private GameObject _inventoryItemInRangeDisplay;
    [SerializeField] private DisplayItemPickup _displayItemPickup;
    public  Action OnAddPickUpItemToInventory;
    public List<ItemPickedUp> ListItemsInRange
    {
        get => _listItemsInRange;   
        set => _listItemsInRange = value;
    }

    public DisplayItemPickup DisplayItemPickup
    {
        get => _displayItemPickup;
        set => _displayItemPickup = value;
    }

    private void Update()
    {
        _inventoryItemInRangeDisplay.SetActive(_listItemsInRange.Count > 0);
    }

    public void AddToInventory(ItemPickedUp item, int amount)
    {
        if (!_inventoryHolder.InventorySystem.AddToInventory(item.ItemData, amount)) return;

        // Create a temporary list to hold items to be removed
        List<ItemPickedUp> itemsToRemove = new List<ItemPickedUp>();

        // Add all items with the same ID to the list
        foreach (var listItem in _listItemsInRange)
        {
            if (listItem.ItemData.ID == item.ItemData.ID)
            {
                itemsToRemove.Add(listItem);
            }
        }

        // Remove all items in the list from _listItemsInRange
        foreach (var listItem in itemsToRemove)
        {
            _listItemsInRange.Remove(listItem);
            listItem.worldItemData.ReturnToPool(listItem.gameObject);
            listItem._isTriggered = false;
        }
        
        OnAddPickUpItemToInventory?.Invoke();
        //_displayItemPickup.DisplayItems();
    }
}
