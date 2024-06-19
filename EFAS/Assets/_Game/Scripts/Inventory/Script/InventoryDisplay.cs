using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Inventory.Script
{
    public class InventoryDisplay : MonoBehaviour
    {
        public int X_SPACE_BETWEEN_ITEM;
        public int NUMBER_OF_COLUMNS;
        public int Y_SPACE_BETWEEN_ITEM;
        [SerializeField] private GameObject inventoryUI;
        public GameObject inventoryPrefab;
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
                    var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
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

                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.database.GetItem[slot.item.Id].uiDisplay;
                //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(slot, obj);
            }
        }
        
        public Vector3 GetPosition(int i)
        {
            return new Vector3(X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMNS),
                -Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMNS), 0f);
        }

        public void CloseInventory()
        {
            inventoryUI.SetActive(false);
        }

        public void OpenInventory()
        {
            inventoryUI.SetActive(true);
        }
    }
}