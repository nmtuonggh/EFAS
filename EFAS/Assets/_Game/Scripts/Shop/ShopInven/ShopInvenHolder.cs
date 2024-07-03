using System;
using UnityEngine;

namespace _Game.Scripts.Shop
{
    public class ShopInvenHolder : MonoBehaviour
    {
        [SerializeField] private int _shopInvenSize;
        public ShopInvenSystem _shopInvenSystem;

        public ShopInvenSystem InvenSystem
        {
            get => _shopInvenSystem;
            set => _shopInvenSystem = value;
        }

        private void Awake()
        {
            _shopInvenSystem = new ShopInvenSystem(_shopInvenSize);
        }
    }
}