using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory;
using _Game.Scripts.Inventory.Item;
using _Game.Scripts.Inventory.Item.Scripts;
using _Game.Scripts.Inventory.Script;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public ItemUI SelectedItem { get; set; }

    public List<GameObject> itemPrefabs;
    public InventoryObject inventoryObject;
    public InventoryDisplay inventoryDisplay;
    
    [SerializeField] private Transform spawnPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DropSelectedItem()
    {
        if (SelectedItem != null)
        {
            InventorySlot slot = SelectedItem._inventorySlot;
            inventoryObject.DropItem(slot.item);
            inventoryDisplay.RemoveItemDisplay(slot);
            SelectedItem = null;
            
            SpawnItem(slot.item);
        }
    }
    
    private void SpawnItem(Item item)
    {
        foreach (GameObject prefab in itemPrefabs)
        {
            GroundItem groundItem = prefab.GetComponent<GroundItem>();
            if (groundItem.item.Id == item.Id)
            {
                Instantiate(prefab, spawnPosition.position, Quaternion.identity);
                break;
            }
        }
    }
}