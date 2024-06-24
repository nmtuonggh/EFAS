using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    [SerializeField] private List<ItemPickedUp> _itemsInRange = new List<ItemPickedUp>();
    
    private void OnTriggerEnter(Collider other)
    {
        ItemPickedUp item = other.GetComponent<ItemPickedUp>();
        if (other.GetComponent<ItemPickedUp>())
        {
            _itemsInRange.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemPickedUp item = other.GetComponent<ItemPickedUp>();
        if (other.GetComponent<ItemPickedUp>())
        {
            _itemsInRange.Remove(item);
        }
    }
}
