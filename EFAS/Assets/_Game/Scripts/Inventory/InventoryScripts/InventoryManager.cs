using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory.Action;
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
    [SerializeField] private DropWhileHolding _dropItemWhileHolding;
    public bool isHolding;

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
        _dropItemWhileHolding.OnDropItemHoldUpdate += UpdateDropButtonState;
    }
    
    private void UpdateDropButtonState(int itemCount)
    {
        // Giả sử `btnDrop` là một instance của `Button`
        dropHoldButton.SetActive(itemCount > 0);
        isHolding = itemCount > 0;
    }
}
