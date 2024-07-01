using System;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using UnityEngine;
    public class HoldeItem : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        //public event Action<InventorySlot> OnHoldeItemSlotChangedEvent;
        public event Action<int> OnItemHoldCountChanged;
        public GameEvent<InventorySlot> OnHoldeItemSlotChanged;
        
        public void OnHoldItem()
        {
            if (_blackBoardInventory.staticInventoryDisplay.FocusSlot != null && _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null && _blackBoardInventory._previewHolder.ItemCount < 4)
            {
                _blackBoardInventory.spawnWorldItem.SpawnToPreview(_blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID, _blackBoardInventory._previewHolder.ItemCount);
                _blackBoardInventory.spawnWorldItem.SpawnToPlayer(_blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID, _blackBoardInventory._previewHolder.ItemCount);
                _blackBoardInventory._previewHolder.ItemCount += 1;
                OnItemHoldCountChanged?.Invoke(_blackBoardInventory._previewHolder.ItemCount);
                InventorySlot selectedSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot;
                if (_blackBoardInventory.inventoryHolder.InventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    //OnHoldeItemSlotChangedEvent?.Invoke(selectedSlot);
                    OnHoldeItemSlotChanged.Raise(selectedSlot);
                }
            }
        }
    }