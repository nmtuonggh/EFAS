using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager :MonoBehaviour
{   
    [Header("UI Elements")]
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject ControlUI;
    [SerializeField] private GameObject PickupUI;
    [SerializeField] private GameObject dropButton; 
    
    [SerializeField] protected StaticInventoryDisplay staticInventoryDisplay;
    [SerializeField] private SpawnWorldItem spawnWorldItem;

    [Header("Spawn Item")]
    [SerializeField] private PreviewHolder _previewHolder;
    //[SerializeField] private PreviewHolder _previewHolder;
    
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

    #region On Off Inventory

    public void OpenInventory()
    {
        InventoryUI.SetActive(true);
        ControlUI.SetActive(false);
        PickupUI.SetActive(false);
    }
    
    public void CloseInventory()
    {
        InventoryUI.SetActive(false);
        ControlUI.SetActive(true);
        PickupUI.SetActive(true);
    }

    #endregion
    
    #region DropItem

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

    #endregion    
    
    public void HoldItem()
    {
        if (staticInventoryDisplay.FocusSlot != null && staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null && _previewHolder.ItemCount < 4)
        {
            spawnWorldItem.SpawnToPreview(staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID, _previewHolder.ItemCount);
            spawnWorldItem.SpawnToPlayer(staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID, _previewHolder.ItemCount);
            _previewHolder.ItemCount += 1;
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
}
