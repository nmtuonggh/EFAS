using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using _Game.Scripts.Inventory.Action;
using UnityEngine;
using UnityEngine.Events;
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
    
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private SpawnWorldItem spawnWorldItem;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private HoldeItem _holdeItem;
    [SerializeField] private DropWhileHolding _dropItemWhileHolding;

    [Header("Spawn Item")]
    [SerializeField] private PreviewHolder _previewHolder;
    
    public static InventoryManager Instance;
    private string savePath = "Assets/_Game/Data/inventoryData.json";
    [SerializeField] private List<InventoryItemData> _listInventoryItemData;
    [SerializeField] private InventorySaveData inventorySaveData;
    
    public GameEventListener UnHoldingState;

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
        //UnHoldingState.OnEnable();
    }
    
    private void OnDestroy()
    {
        //UnHoldingState.OnDisable();
    }
    
    public InventorySaveData ToSaveData()
    {
        var saveData = new InventorySaveData();
        saveData.inventorySlotSaveDatas = new List<InventorySaveData.InventorySlotSaveData>();

        foreach (var slot in inventoryHolder.InventorySystem.InventorySlots)
        {
            var slotSaveData = new InventorySaveData.InventorySlotSaveData();
            slotSaveData.itemID = slot.ItemData?.ID ?? -1;
            slotSaveData.stackSize = slot.StackSize;
            saveData.inventorySlotSaveDatas.Add(slotSaveData);
        }

        return saveData;
    }

    public void SaveToFile()
    {
        var saveData = ToSaveData();
        var json = JsonUtility.ToJson(saveData);

        System.IO.File.WriteAllText(savePath, json);
    }
    
    public void LoadFromSaveData(InventorySaveData saveData)
    {
        foreach (var slotSaveData in saveData.inventorySlotSaveDatas)
        {
            inventoryHolder.InventorySystem.AddToInventory(
                slotSaveData.itemID >= 0 ? GetItemDataByID(slotSaveData.itemID) : null, slotSaveData.stackSize);
        }
    }
    
    
    public InventoryItemData GetItemDataByID(int id)
    {
        foreach (var item in _listInventoryItemData)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null; 
    }

    public void LoadFromFile()
    {
        var json = System.IO.File.ReadAllText(savePath);
        var saveData = JsonUtility.FromJson<InventorySaveData>(json);
        LoadFromSaveData(saveData);
    }
}
