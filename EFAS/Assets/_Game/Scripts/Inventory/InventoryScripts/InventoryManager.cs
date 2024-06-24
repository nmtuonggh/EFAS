using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject ControlUI;
    [SerializeField] public InventorySystem _inventorySystem;
    [SerializeField] protected StaticInventoryDisplay staticInventoryDisplay;

    public void OpenInventory()
    {
        InventoryUI.SetActive(true);
        ControlUI.SetActive(false);
    }
    
    public void CloseInventory()
    {
        InventoryUI.SetActive(false);
        ControlUI.SetActive(true);
    }

    
}
