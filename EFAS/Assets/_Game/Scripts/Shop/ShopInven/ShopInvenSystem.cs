using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Shop
{
    public class ShopInvenSystem
    {
        public List<ShopInvenSlot> _shopInvenSlots;
        public int ShopInvenSize => _shopInvenSlots.Count;

        public List<ShopInvenSlot> ShopInvenSlots
        {
            get => _shopInvenSlots;
            set => _shopInvenSlots = value;
        }
        
        public ShopInvenSystem(int size)
        {
            _shopInvenSlots = new List<ShopInvenSlot>(size);
            for (int i = 0; i < size; i++)
            {
                _shopInvenSlots.Add(new ShopInvenSlot());
            }
        }
    }
}