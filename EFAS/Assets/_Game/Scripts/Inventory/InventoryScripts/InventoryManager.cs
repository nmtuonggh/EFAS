using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager :MonoBehaviour
{
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject ControlUI;
    [SerializeField] public InventoryHolder _InventoryHolder;
    [SerializeField] protected StaticInventoryDisplay staticInventoryDisplay;
    [SerializeField] private SpawnWorldItem spawnWorldItem;
    [SerializeField] private GameObject dropButton; 
    public static InventoryManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
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
    
    public void DropItem()
    {
        if (staticInventoryDisplay.FocusSlot != null && staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null)
        {
            spawnWorldItem.SpawnDropItem(staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID);

            InventorySlot selectedSlot = staticInventoryDisplay.FocusSlot.AssingnedInventorySlot;

            if (selectedSlot.StackSize > 1)
            {
                selectedSlot.RemoveFromStack(1);
                staticInventoryDisplay.FocusSlot.UpdateUISlot();
            }
            else
            {
                selectedSlot.ClearData();
                staticInventoryDisplay.FocusSlot.UpdateUISlot();
            }
        }
    }
    
    public void DropAllItemsInSlot()
    {
        if (staticInventoryDisplay.FocusSlot != null && staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null)
        {
            InventorySlot selectedSlot = staticInventoryDisplay.FocusSlot.AssingnedInventorySlot;
            int stackSize = selectedSlot.StackSize;

            for (int i = 0; i < stackSize; i++)
            {
                spawnWorldItem.SpawnDropItem(staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID);
                selectedSlot.RemoveFromStack(1);
            }

            selectedSlot.ClearData();
            staticInventoryDisplay.FocusSlot.UpdateUISlot();
        }
    }
    
}
