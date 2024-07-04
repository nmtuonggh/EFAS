using System.Collections.Generic;
using _Game.Scripts.Shop;
using UnityEngine;

namespace _Game.Scripts.Ebook
{
    public class EbookSystem
    {
        public List<EbookSlot> _ebookSlots;
        public int EbookSize => _ebookSlots.Count;

        public List<EbookSlot> EbookSlots
        {
            get => _ebookSlots;
            set => _ebookSlots = value;
        }
        
        public EbookSystem(int size)
        {
            _ebookSlots = new List<EbookSlot>(size);
            for (int i = 0; i < size; i++)
            {
                _ebookSlots.Add(new EbookSlot());
            }
        }
    }
}