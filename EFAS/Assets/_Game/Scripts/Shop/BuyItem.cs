using System;
using _Game.Scripts.Inventory;
using UnityEngine;

namespace _Game.Scripts.Shop
{
    public class BuyItem : MonoBehaviour
    {
        [SerializeField] private  ShopDisplay _shopDisplay;
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        public event Action<int> OnBuyItemEvent;
        public static event System.Action OnOutOfMoney;
        public Money Money;
        
        public void OnBuyItem()
        {
            var currentSlot = _shopDisplay.FocusSlot;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem; 
            if (currentSlot != null && currentSlot.AssingnedInventorySlot.Data != null)
            {
                ShopInvenSlot selectedSlot = currentSlot.AssingnedInventorySlot;
                if (!(selectedSlot.Data.Price > Money.MoneyAmount))
                {
                    if (inventorySystem.AddToInventory(selectedSlot.Data, 1))
                    {
                        OnBuyItemEvent?.Invoke(selectedSlot.Data.Price);
                    }
                }else
                {
                    OnOutOfMoney?.Invoke();
                }
            }
        }
    }
}