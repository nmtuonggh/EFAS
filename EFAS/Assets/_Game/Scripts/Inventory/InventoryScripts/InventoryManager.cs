using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using _Game.Scripts.Inventory.Action;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager :MonoBehaviour
{
    [SerializeField] private InventoryHolder inventoryHolder;
    public static InventoryManager Instance;
    private string savePath = "Assets/_Game/SaveData/inventoryData.json";
    [SerializeField] private List<ItemData> _listItemData;

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
    
    
    public ItemData GetItemDataByID(int id)
    {
        foreach (var item in _listItemData)
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
