using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickedUp : MonoBehaviour
{
    /*public InventoryItemData ItemData;
    public WorldItemData WorldItemData;
    public void OnTriggerEnter(Collider other)
    {
        var inventoryHolder = other.GetComponent<InventoryHolder>();
        if(!inventoryHolder) return;
        if (inventoryHolder.InventorySystem.AddToInventory(ItemData, 1))
        {
            Debug.Log("pick");
            Destroy(this.gameObject);
            WorldItemData.ReturnToPool(this.gameObject);
        }
    }*/
    
    public InventoryItemData ItemData;
    public WorldItemData worldItemData;
    private bool _isTriggered;
    public void OnTriggerEnter(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if(itemPickupItemSystem != null && !_isTriggered)
        {
            _isTriggered = true;
            itemPickupItemSystem.ItemsInRange.Add(this);
            itemPickupItemSystem.DisplayItems();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if(itemPickupItemSystem!= null)
        {
            _isTriggered = false;
            itemPickupItemSystem.ItemsInRange.Remove(this);
            itemPickupItemSystem.DisplayItems();
        }
    }
}
