using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public Inventory Container;
    public List<InventorySlot> Slots = new List<InventorySlot>();

    public void Add_Item(UI_ItemObject uiItem, int amount)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].item == uiItem.item)
            {
                Slots[i].AddAmount(amount);
                return;
            }
        }
    }
}
