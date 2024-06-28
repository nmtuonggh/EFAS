using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool isHolding;

    [Header("Spawn Item")]
    [SerializeField] private PreviewHolder _previewHolder;
    
    public static InventoryManager Instance;
    private string savePath = "Assets/_Game/Data/inventoryData.json";
    [SerializeField] private List<InventoryItemData> _listInventoryItemData;
    //public event Action<InventorySlot> OnLoadingData;

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
        dropHoldButton.SetActive(itemCount > 0);
        isHolding = itemCount > 0;
    }
    
    public InventorySaveData ToSaveData()
    {
        var saveData = new InventorySaveData();
        saveData.inventorySlots = new List<InventorySaveData.InventorySlotSaveData>();

        foreach (var slot in inventoryHolder.InventorySystem.InventorySlots)
        {
            var slotSaveData = new InventorySaveData.InventorySlotSaveData();
            slotSaveData.itemID = slot.ItemData?.ID ?? -1;
            slotSaveData.stackSize = slot.StackSize;
            saveData.inventorySlots.Add(slotSaveData);
        }

        return saveData;
    }

    public void SaveToFile()
    {
        var saveData = ToSaveData();
        var json = JsonUtility.ToJson(saveData);
        Debug.Log("ghi file" + json);

        System.IO.File.WriteAllText(savePath, json);
    }
    
    public void LoadFromSaveData(InventorySaveData saveData)
    {
        inventoryHolder.InventorySystem.InventorySlots.Clear();

        foreach (var slotSaveData in saveData.inventorySlots)
        {
            var slot = new InventorySlot();
            slot.ItemData = slotSaveData.itemID >= 0 ? GetItemDataByID(slotSaveData.itemID) : null;
            slot.StackSize = slotSaveData.stackSize;
            inventoryHolder.InventorySystem.InventorySlots.Add(slot);
           // OnLoadingData?.Invoke(slot);
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
