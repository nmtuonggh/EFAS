using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class HoldeItem : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;

        public void OnHoldItem()
        {
            if (_blackBoardInventory.staticInventoryDisplay.FocusSlot != null && _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData != null && _blackBoardInventory._previewHolder.ItemCount < 4)
            {
                _blackBoardInventory.spawnWorldItem.SpawnToPreview(_blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID, _blackBoardInventory._previewHolder.ItemCount);
                _blackBoardInventory.spawnWorldItem.SpawnToPlayer(_blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot.ItemData.ID, _blackBoardInventory._previewHolder.ItemCount);
                _blackBoardInventory._previewHolder.ItemCount += 1;
                InventorySlot selectedSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot.AssingnedInventorySlot;
                if (_blackBoardInventory.inventoryHolder.InventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    _blackBoardInventory.inventoryHolder.InventorySystem.OnInventorySlotChanged?.Invoke(selectedSlot);
                }
            }
        }
    }
}