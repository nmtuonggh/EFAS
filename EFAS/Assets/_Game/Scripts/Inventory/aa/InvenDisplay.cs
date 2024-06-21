using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenDisplay : MonoBehaviour
{
    public Inventory inventory;
    private bool _itemPickedUp = false;
    public void OnTriggerEnter(Collider other)
    {
        if(_itemPickedUp) return;
        var item = other.gameObject.GetComponent<World_ItemObject>();
        if (item)
        {
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
