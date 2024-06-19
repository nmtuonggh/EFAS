using System.Collections.Generic;
using _Game.Scripts.Inventory.Item.Scripts;
using UnityEngine;
namespace _Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public List<InventorySlot> Container = new List<InventorySlot>();

        public void AddItem(BaseItemObject item, int amount)
        {
            bool hasItem = false;
            for (int i = 0; i < Container.Count; i++)
            {
                if (Container[i].item == item)
                {
                    Container[i].AddAmount(amount);
                    hasItem = true;
                    break;
                }
            }

            if (!hasItem)
            {
                Container.Add(new InventorySlot(item, amount));
            }
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public BaseItemObject item;
        public int amount;

        public InventorySlot(BaseItemObject _item, int _amount)
        {
            item = _item;
            amount = _amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}