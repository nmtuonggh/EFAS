using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickedUp : MonoBehaviour
{
    public InventoryItemData ItemData;

    public void OnTriggerEnter(Collider other)
    {
        var inventoryHolder = other.GetComponent<InventoryHolder>();
        if(!inventoryHolder) return;
        if (inventoryHolder.InventorySystem.AddToInventory(ItemData, 1))
        {
            Debug.Log("pick");
            Destroy(this.gameObject);
        }
    }
}
