using _Game.Scripts.Event;
using UnityEngine;

namespace _Game.Scripts.Inventory.InventoryAction
{
    public class Eat : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        public GameEventT<float> OnEat;
        public GameEventT<InventorySlot> OnEatInventoryItem;
        public static event System.Action OnEatSound;
        
        public void OnEatItem()
        {
            var currentSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem; 
            if (currentSlot != null && currentSlot.AssingnedInventorySlot.ItemData != null)
            {
                OnEat.Raise(currentSlot.AssingnedInventorySlot.ItemData.HungerValue);
                InventorySlot selectedSlot = currentSlot.AssingnedInventorySlot;
                if (inventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    OnEatSound?.Invoke();
                    OnEatInventoryItem.Raise(selectedSlot);
                }
            }
        }
    }
}