using System;
using _Game.Scripts.Event;
using _Game.Scripts.Inventory;
using UnityEngine;
    public class HoldeItem : MonoBehaviour
    {
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        [SerializeField] private Transform _poolItemsHoldInPlayer; 
        //public event Action<int> OnItemHoldCountChanged;
        
        public GameEventT<InventorySlot> OnHoldeItemSlotChanged;
        public GameEventListener OnOutInventory;

        private void Awake()
        {
            OnOutInventory.OnEnable();
        }
        
        private void OnDisable()
        {
            OnOutInventory.OnDisable();
        }

        public void OnHoldItem()
        {
            var currentTouchSlot = _blackBoardInventory.staticInventoryDisplay.FocusSlot;
            var inventorySystem = _blackBoardInventory.inventoryHolder.InventorySystem;
            
            if (currentTouchSlot != null && currentTouchSlot.AssingnedInventorySlot.ItemData != null && _blackBoardInventory._previewHolder.ItemCount < 4)
            {
                _blackBoardInventory.spawnWorldItem.SpawnToPreview(currentTouchSlot.AssingnedInventorySlot.ItemData.ID, _blackBoardInventory._previewHolder.ItemCount);
                _blackBoardInventory.spawnWorldItem.SpawnToPlayer(currentTouchSlot.AssingnedInventorySlot.ItemData.ID, _blackBoardInventory._previewHolder.ItemCount);
                _blackBoardInventory._previewHolder.ItemCount += 1;
                //OnItemHoldCountChanged?.Invoke(_blackBoardInventory._previewHolder.ItemCount);
                InventorySlot selectedSlot = currentTouchSlot.AssingnedInventorySlot;
                if (inventorySystem.RemoveFromInventory(selectedSlot, selectedSlot.ItemData, 1))
                {
                    OnHoldeItemSlotChanged.Raise(selectedSlot);
                }
            }
            foreach (Transform child in _poolItemsHoldInPlayer)
            {
                if (child.GetComponent<ItemPickedUp>() != null)
                {
                    //child.GetComponent<Rigidbody>().isKinematic = true;
                    child.tag = "ItemHolding";
                }
            }
        }

        public void SetKinematicHoldItem()
        {
            foreach (Transform child in _poolItemsHoldInPlayer)
            {
                if (child.GetComponent<ItemPickedUp>() != null && child.GetComponent<ItemPickedUp>().isActiveAndEnabled)
                {
                    child.GetComponent<Rigidbody>().isKinematic = true;
                    //child.tag = "ItemHolding";
                }
            }
        }
    }