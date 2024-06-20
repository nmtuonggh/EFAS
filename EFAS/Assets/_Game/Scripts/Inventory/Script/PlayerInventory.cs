using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory;
using _Game.Scripts.Inventory.Item;
using _Game.Scripts.Inventory.Item.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public BlackBoard blackBoard;
    private bool _itemPickedUp = false;

    public void OnTriggerEnter(Collider other)
    {
        if(_itemPickedUp) return;
        var item = other.gameObject.GetComponent<GroundItem>();
        if (item)
        {
            Debug.Log("run");
            inventory.AddItem(new Item(item.item),1); 
            Destroy(other.gameObject);
            _itemPickedUp = true;
            StartCoroutine(ResetTrigger());
        }
    }

    IEnumerator ResetTrigger()
    {
        yield return new WaitForEndOfFrame();
        _itemPickedUp = false;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
    
}
