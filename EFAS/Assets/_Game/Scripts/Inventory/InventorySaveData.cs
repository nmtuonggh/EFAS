using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Inventory
{
    [System.Serializable]
public class InventorySaveData
{
    public List<InventorySlotSaveData> inventorySlots;

    [System.Serializable]
    public class InventorySlotSaveData
    {
        public int itemID;
        public int stackSize;
    }
}
}