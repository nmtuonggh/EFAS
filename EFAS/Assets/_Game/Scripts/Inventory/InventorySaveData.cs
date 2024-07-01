using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Inventory
{
    [System.Serializable]
public class InventorySaveData
{
    [FormerlySerializedAs("inventorySlots")] public List<InventorySlotSaveData> inventorySlotSaveDatas;

    [System.Serializable]
    public class InventorySlotSaveData
    {
        public int itemID;
        public int stackSize;
    }
}
}