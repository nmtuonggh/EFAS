using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Inventory.Script
{
    public class InventoryDisplay : MonoBehaviour
    {
        public GameObject inventoryPrefab;

        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private GameObject movementUI;
        [SerializeField] private InventoryObject inventory;
        Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

        private void Start()
        {
            CreateDisplay();
        }

        private void Update()
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            for (int i = 0; i < inventory.Container.Items.Count; i++)
            {
                InventorySlot slot = inventory.Container.Items[i];
                if (itemsDisplayed.ContainsKey(slot))
                {
                    itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                }
                else
                {
                    var obj = Instantiate(inventoryPrefab, transform);
                    obj.GetComponent<ItemUI>()._inventorySlot = slot;

                    obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                        inventory.database.GetItem[slot.item.Id].uiDisplay;
                    //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

                    itemsDisplayed.Add(slot, obj);
                }
            }
        }

        private void CreateDisplay()
        {
            for (int i = 0; i < inventory.Container.Items.Count; i++)
            {
                InventorySlot slot = inventory.Container.Items[i];

                var obj = Instantiate(inventoryPrefab, transform);
                obj.GetComponent<ItemUI>()._inventorySlot = slot;
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(slot, obj);
            }
        }

        public void RemoveItemDisplay(InventorySlot slot)
        {
            if (itemsDisplayed.ContainsKey(slot))
            {
                GameObject itemDisplay = itemsDisplayed[slot];
                itemsDisplayed.Remove(slot);
                Destroy(itemDisplay);
            }
        }

        public void CloseInventory()
        {
            inventoryUI.SetActive(false);
            movementUI.SetActive(true);
        }

        public void OpenInventory()
        {
            inventoryUI.SetActive(true);
            movementUI.SetActive(false);
        }
    }
}