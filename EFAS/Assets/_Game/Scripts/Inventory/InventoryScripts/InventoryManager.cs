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
    [SerializeField] private GameObject dropHoldButton; 
    
    [SerializeField] protected StaticInventoryDisplay staticInventoryDisplay;
    [SerializeField] private SpawnWorldItem spawnWorldItem;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private HoldeItem _holdeItem;

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

        _holdeItem.OnItemHoldCountChanged += UpdateDropButtonState;
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
    
    private void UpdateDropButtonState(int itemCount)
    {
        // Giả sử `btnDrop` là một instance của `Button`
        dropHoldButton.SetActive(itemCount > 0);
    }
}
