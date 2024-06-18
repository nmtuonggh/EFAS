using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Inventory.Script
{
    public class InventoryDisplay : MonoBehaviour
    {
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
            for (int i = 0; i < inventory.Container.Count; i++)
            {
                if (itemsDisplayed.ContainsKey(inventory.Container[i]))
                {
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                        inventory.Container[i].amount.ToString("n0");
                }
                else
                {
                    Debug.Log("else");
                    Debug.Log(inventory.Container[i].item.prefab.name);
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }

        private void CreateDisplay()
        {
            for (int i = 0; i < inventory.Container.Count; i++)
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
}