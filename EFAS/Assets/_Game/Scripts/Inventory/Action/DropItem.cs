using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;

        public void OnDropItem()
        {
            if (_blackBoardInventory.staticInventoryDisplay.FocusSlot != null &&
                _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null)
            {
                _blackBoardInventory.spawnWorldItem.SpawnDropItem(_blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID);

                InventorySlot selectedSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot;

                if (_blackBoardInventory.inventoryHolder.InventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    _blackBoardInventory.inventoryHolder.InventorySystem.OnInventorySlotChanged?.Invoke(selectedSlot);
                }
            }
        }

        public void OnDropAllItemsInSlot()
        {
            if (_blackBoardInventory.staticInventoryDisplay.FocusSlot != null &&
                _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null)
            {
                InventorySlot selectedSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot;
                int stackSize = selectedSlot.StackSize;
                for (int i = 0; i < stackSize; i++)
                {
                    _blackBoardInventory.spawnWorldItem.SpawnDropItem(
                        _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID);
                }
                if (_blackBoardInventory.inventoryHolder.InventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, stackSize))
                {
                    _blackBoardInventory.inventoryHolder.InventorySystem.OnInventorySlotChanged?.Invoke(selectedSlot);
                }
            }
        }
    }
}