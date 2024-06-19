using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory;
using _Game.Scripts.Inventory.Item;
using _Game.Scripts.Inventory.Item.Scripts;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public BlackBoard blackBoard;
    
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item),1); 
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }

    
    
    
}
