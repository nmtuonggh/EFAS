using UnityEngine;

namespace _Game.Scripts.Ebook
{
    public class EbookSlot
    {
        [SerializeField] private ItemData _itemData;

        public ItemData Data
        {
            get => _itemData;
            set => _itemData = value;
        }

        public EbookSlot(ItemData data)
        {
            _itemData = data;
        }

        public EbookSlot() { }
    }
}