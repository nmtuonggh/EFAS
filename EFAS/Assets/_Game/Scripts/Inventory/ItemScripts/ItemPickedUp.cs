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
    //public PickupItemSystem pickupItemSystem;
    public WorldItemData worldItemData;
    public void OnTriggerEnter(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if(itemPickupItemSystem != null)
        {
            itemPickupItemSystem.ItemsInRange.Add(this);
            itemPickupItemSystem.DisplayItems();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var itemPickupItemSystem = other.GetComponent<PickupItemSystem>();
        if(itemPickupItemSystem!= null)
        {
            itemPickupItemSystem.ItemsInRange.Remove(this);
            itemPickupItemSystem.DisplayItems();
        }
    }
}
