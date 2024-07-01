using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        //public event Action<InventorySlot> OnDropItemUpdate;
        public GameEvent<InventorySlot> OnDropInventoryItem;
        
        public void OnDropItem()
        {
            var currentSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem; 
            if (currentSlot != null && currentSlot.AssingnedInventorySlot.ItemData != null)
            {
                _blackBoardInventory.spawnWorldItem.SpawnDropItem(currentSlot.AssingnedInventorySlot.ItemData.ID);
                InventorySlot selectedSlot = currentSlot.AssingnedInventorySlot;

                if (inventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    //OnDropItemUpdate?.Invoke(selectedSlot);
                    OnDropInventoryItem.Raise(selectedSlot);
                }
            }
        }

        public void OnDropAllItemsInSlot()
        {
            var currentSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem;
            if (currentSlot != null && currentSlot.AssingnedInventorySlot.ItemData != null)
            {
                InventorySlot selectedSlot = currentSlot.AssingnedInventorySlot;
                int stackSize = selectedSlot.StackSize;
                for (int i = 0; i < stackSize; i++)
                {
                    _blackBoardInventory.spawnWorldItem.SpawnDropItem(currentSlot.AssingnedInventorySlot.ItemData.ID);
                }

                if (inventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, stackSize))
                {
                    //OnDropItemUpdate?.Invoke(selectedSlot);
                    OnDropInventoryItem.Raise(selectedSlot);
                }
            }
        }
    }
}