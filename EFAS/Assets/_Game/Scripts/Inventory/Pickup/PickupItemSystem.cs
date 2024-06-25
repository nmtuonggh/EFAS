using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupItemSystem : MonoBehaviour
{
    [SerializeField] private List<ItemPickedUp> _itemsInRange = new List<ItemPickedUp>();
    [SerializeField] private GameObject itemDisplayPrefab;
    [SerializeField] private Transform itemsInRangeHolder;
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private GameObject _inventoryItemInRangeDisplay;
    
    public List<ItemPickedUp> ItemsInRange
    {
        get => _itemsInRange;   
        set => _itemsInRange = value;
    }

    private void Update()
    {
        _inventoryItemInRangeDisplay.SetActive(_itemsInRange.Count > 0);
    }

    public void DisplayItems()
    {
        foreach (Transform child in itemsInRangeHolder)
        {
            Destroy(child.gameObject);
        }
        // Display new items
        foreach (var item in _itemsInRange)
        {
            var itemDisplay = Instantiate(itemDisplayPrefab, itemsInRangeHolder);
            var imageChild = itemDisplay.transform.GetChild(2);
            itemDisplay.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemData.DisplayName;
            imageChild.GetComponentInChildren<Image>().sprite = item.ItemData.Icon;
            
            var button = itemDisplay.GetComponent<Button>();
            button.onClick.AddListener(() => AddToInventory(item));
        }
    }

    private void AddToInventory(ItemPickedUp item)
    {
        if (!_inventoryHolder.InventorySystem.AddToInventory(item.ItemData, 1)) return;
        _itemsInRange.Remove(item);
        DisplayItems();
        item.worldItemData.ReturnToPool(item.gameObject);
        item._isTriggered = false;
    }
}
