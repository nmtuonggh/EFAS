using System;
using System.Collections.Generic;
using _Game.Scripts.Shop;
using UnityEngine;

namespace _Game.Scripts.Ebook
{
    public class EbookDisplay : MonoBehaviour
    {
            public EbookHolder EbookHolder;
            public EbookSlot_UI[] slots;
            public EbookSlot_UI _focusSlot;
            public Dictionary<EbookSlot_UI, EbookSlot> slotsDictionary;
            public List<ItemData> _ebookItems;
            public event Action<EbookSlot_UI> EbookSlotClick; 
        
            public EbookSlot_UI FocusSlot
            {
                get => _focusSlot;
                set => _focusSlot = value;
            }
        
            private void Start()
            {
                AssignSlot(EbookHolder.EbookSystem);
                AssignDataToSlots(); 
        
                foreach (var slot in slotsDictionary)
                {
                    slot.Key.UpdateEbookUISlot(slot.Value);
                }
            }
        
            public void AssignSlot(EbookSystem ebookSystem)
            {
                slotsDictionary = new Dictionary<EbookSlot_UI, EbookSlot>();
        
                if (slots.Length != ebookSystem.EbookSize)
                {
                    Debug.LogError("Slot length does not match the inventory size");
                    return;
                }
        
                for (int i = 0; i < ebookSystem.EbookSize; i++)
                {
                    slotsDictionary.Add(slots[i], ebookSystem.EbookSlots[i]);
                    slots[i].Init(ebookSystem.EbookSlots[i]);
                }
            }
        
            public void AssignDataToSlots() 
            {
                if (_ebookItems.Count != slotsDictionary.Count)
                {
                    Debug.LogError("The number of items does not match the number of slots");
                    return;
                }
        
                for (int i = 0; i < _ebookItems.Count; i++)
                {
                    EbookSlot slot = slotsDictionary[slots[i]];
                    slot.Data = _ebookItems[i];
                    slots[i].UpdateEbookUISlot(slot);
                }
            }
        
            public void EbookSlotClicked(EbookSlot_UI clickedUISlot)
            {
                SetFocus(clickedUISlot);
                EbookSlotClick?.Invoke(clickedUISlot);
            }
        
            private void SetFocus(EbookSlot_UI clickedUISlot)
            {
                if (_focusSlot != null)
                {
                    _focusSlot.FocusLine.SetActive(false);
                }
                
                if (clickedUISlot.AssingnedInventorySlot.Data != null)
                {
                    _focusSlot = clickedUISlot;
                    _focusSlot.FocusLine.SetActive(true);
                }
            }
    }
}