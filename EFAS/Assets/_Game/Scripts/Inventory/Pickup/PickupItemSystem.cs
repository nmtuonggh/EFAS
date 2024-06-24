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
    
    public List<ItemPickedUp> ItemsInRange
    {
        get => _itemsInRange;   
        set => _itemsInRange = value;
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
            itemDisplay.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemData.DisplayName;
            itemDisplay.GetComponentInChildren<Image>().sprite = item.ItemData.Icon;
        }
    }
}
