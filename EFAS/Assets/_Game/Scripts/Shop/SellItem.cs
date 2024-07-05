using System;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Shop
{
    public class SellItem : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        public UnityEvent<int> OnSellItemEvent;
        public static event System.Action AudioSellItem;

        public void OnSellItem()
        {
            var currentSlot = _blackBoardInventory.staticInventoryDisplay.CurrentClick;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem;
            if (currentSlot != null && currentSlot.AssingnedInventorySlot.ItemData != null)
            {
                //TODO: + money
                InventorySlot selectedSlot = currentSlot.AssingnedInventorySlot;
                var price = selectedSlot.ItemData.Price;

                if (inventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    AudioSellItem?.Invoke();
                    OnSellItemEvent?.Invoke(price);
                }
            }
        }

        public void OnSellAllItem()
        {
            var currentSlot = _blackBoardInventory.staticInventoryDisplay.CurrentClick;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem;
            if (currentSlot != null && currentSlot.AssingnedInventorySlot.ItemData != null)
            {
                
                InventorySlot selectedSlot = currentSlot.AssingnedInventorySlot;
                var value = selectedSlot.ItemData.Price * selectedSlot.StackSize;

                if (inventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, selectedSlot.StackSize))
                {
                    AudioSellItem?.Invoke();
                    OnSellItemEvent?.Invoke(value);
                }
            }
        }
    }
}