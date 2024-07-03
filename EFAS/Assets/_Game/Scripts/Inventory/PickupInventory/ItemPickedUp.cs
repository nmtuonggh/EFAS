using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemPickedUp : MonoBehaviour
{
    //[FormerlySerializedAs("ItemData")] public InventoryItemData InventoryItemData;
    //public WorldItemData worldItemData;
    public ItemData itemData;
    public bool _isTriggered = false;
    public event Action OnAddPickUpItemToInventory;
    
    public void OnTriggerEnter(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if (itemPickupItemSystem != null && !_isTriggered && !this.CompareTag("ItemHolding"))
        {
            _isTriggered = true;
            itemPickupItemSystem.ListItemsInRange.Add(this);
            itemPickupItemSystem.DisplayItemPickup.DisplayItems();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if (itemPickupItemSystem != null && !this.CompareTag("ItemHolding"))
        {
            _isTriggered = false;
            itemPickupItemSystem.ListItemsInRange.Remove(this);
            itemPickupItemSystem.DisplayItemPickup.DisplayItems();

        }
    }
}