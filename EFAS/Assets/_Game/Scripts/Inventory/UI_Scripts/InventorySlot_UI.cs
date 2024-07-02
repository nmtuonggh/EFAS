using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assingnedInventorySlot;
    [SerializeField] private GameObject focus_Line;

    private Button button;
    
    public InventorySlot AssingnedInventorySlot => assingnedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    public GameObject FocusLine
    {
        get => focus_Line;
        set => focus_Line = value;
    }

    private void Awake()
    {
        ClearSlot();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }
    
    
    public void Init(InventorySlot slot)
    {
        assingnedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;
            
            if(slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }
    
    public void UpdateUISlot()
    {
        if(assingnedInventorySlot !=null) UpdateUISlot(assingnedInventorySlot);
    }

    public void ClearSlot()
    {
        if(assingnedInventorySlot == null)
        {ClearSlot();} 
        itemSprite.sprite = null;   
        itemCount.text = "";
        itemSprite.color = Color.clear;
    }
    
    public void OnUISlotClick()
    {
        //ParentDisplay?.SlotClicked(this);
        if(ParentDisplay!= null) ParentDisplay.SlotClicked(this);
    }
}
