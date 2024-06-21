using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public UI_ItemObject item;
    public int amount;

    public InventorySlot(UI_ItemObject _uiItem, int _amount)
    {
        item = _uiItem;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}