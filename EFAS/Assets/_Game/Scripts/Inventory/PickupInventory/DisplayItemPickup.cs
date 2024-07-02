using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.Scripts.Inventory.PickupInventory
{
    public class DisplayItemPickup : MonoBehaviour
    {
        [SerializeField] private PickupItemSystem _pickupItemSystem;
        [SerializeField] private GameObject itemDisplayPrefab;
        [SerializeField] private Transform itemsInRangeHolder;
        private void Start()
        {
            _pickupItemSystem.OnDisplayPickUpItemToInventory += DisplayItems;
        }

        public void DisplayItems()
        {
            foreach (Transform child in itemsInRangeHolder)
            {
                Destroy(child.gameObject);
            }
            Dictionary<int, (ItemPickedUp itemPickedUp, int stack)> itemStacks = new Dictionary<int, (ItemPickedUp, int)>();
            foreach (var item in _pickupItemSystem.ListItemsInRange)
            {
                if (itemStacks.ContainsKey(item.ItemData.ID))
                {
                    itemStacks[item.ItemData.ID] = (item, itemStacks[item.ItemData.ID].stack + 1);
                }
                else
                {
                    itemStacks.Add(item.ItemData.ID, (item, 1));
                }
            }
            // Display new items
            foreach (var item in itemStacks)
            {
                var itemDisplay = Instantiate(itemDisplayPrefab, itemsInRangeHolder);
                var imageChild = itemDisplay.transform.GetChild(2);
                var stackChild = itemDisplay.transform.GetChild(4);
                itemDisplay.GetComponentInChildren<TextMeshProUGUI>().text = item.Value.itemPickedUp.ItemData.DisplayName;
                imageChild.GetComponentInChildren<Image>().sprite = item.Value.itemPickedUp.ItemData.Icon;
                stackChild.GetComponentInChildren<TextMeshProUGUI>().text = item.Value.stack.ToString();
                
                var button = itemDisplay.GetComponent<Button>();
                button.onClick.AddListener(() => _pickupItemSystem.AddToInventory(item.Value.itemPickedUp, item.Value.stack));
            }
        }
    }
}