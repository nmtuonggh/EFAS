using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class CancelHolding : MonoBehaviour
    {
        [SerializeField] private Transform _poolItemsHoldInPlayer;
        [SerializeField] private Transform _poolItemsHoldInPreview;
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        [SerializeField] private PreviewHolder _previewHolder;
        [SerializeField] private InventoryHolder _inventoryHolder;
        

        public void OnCancelHolding()
        {
            foreach (Transform child in _poolItemsHoldInPlayer)
            {
                var item = child.GetComponent<ItemPickedUp>();
                if (item != null && item.isActiveAndEnabled)
                {
                    //add to inventory
                    _inventoryHolder.InventorySystem.AddToInventory(item.ItemData, 1);
                    //return to pool
                    item.worldItemData.ReturnToPool(child.GetComponent<ItemPickedUp>().gameObject);
                }
            }
            foreach (Transform child in _poolItemsHoldInPreview)
            {
                //return to pool
                var item = child.GetComponent<ItemPickedUp>();
                if (item != null && item.isActiveAndEnabled)
                {
                    item.worldItemData.ReturnToPool(child.GetComponent<ItemPickedUp>().gameObject);
                    _previewHolder.ItemCount = 0;
                }
            }
        }
    }
}