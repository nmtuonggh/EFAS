using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _forcusLine;
    [SerializeField] private bool _isSelecting;
    private static ItemUI SelectedItem;
    public InventorySlot _inventorySlot;
    
    
    public void OnPointerDown(PointerEventData eventData)
{
    
    if (InventoryManager.Instance.SelectedItem != null)
    {
        InventoryManager.Instance.SelectedItem.Deselect();
    }

    InventoryManager.Instance.SelectedItem = this;
    Select();
    
    Debug.Log("Clicked on item with ID: " + _inventorySlot.item.Id);
}

    public void Select()
    {
        _forcusLine.SetActive(true);
        _isSelecting = true;
    }
    
    public void Deselect()
    {
        _forcusLine.SetActive(false);
        _isSelecting = false;
    }
}
