using UnityEngine;

namespace _Game.Scripts.Shop
{
    public class ShopInvenSlot
    {
        [SerializeField] private ItemData _itemData;

        public ItemData Data
        {
            get => _itemData;
            set => _itemData = value;
        }

        public ShopInvenSlot(ItemData data)
        {
            _itemData = data;
        }

        public ShopInvenSlot() { }
    }
}