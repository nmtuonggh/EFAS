using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickedUp : MonoBehaviour
{
    public InventoryItemData ItemData;
    public WorldItemData worldItemData;
    public WorldItemWithoutColliderData worldItemWithoutColliderData;
    public bool _isTriggered = false;
    
    public void OnTriggerEnter(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if (itemPickupItemSystem != null && !_isTriggered && isActiveAndEnabled)
        {
            _isTriggered = true;
            itemPickupItemSystem.ListItemsInRange.Add(this);
            itemPickupItemSystem.OnAddPickUpItemToInventory.Invoke();
            itemPickupItemSystem.DisplayItemPickup.DisplayItems();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if (itemPickupItemSystem != null && isActiveAndEnabled)
        {
            _isTriggered = false;
            itemPickupItemSystem.ListItemsInRange.Remove(this);
            itemPickupItemSystem.DisplayItemPickup.DisplayItems();
        }
    }
}