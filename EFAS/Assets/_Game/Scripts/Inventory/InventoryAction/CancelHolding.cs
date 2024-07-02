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
                if (child.GetComponent<ItemPickedUp>() != null && child.GetComponent<ItemPickedUp>().isActiveAndEnabled)
                {
                    _inventoryHolder.InventorySystem.AddToInventory(child.GetComponent<ItemPickedUp>().ItemData, 1);
                    child.GetComponent<ItemPickedUp>()
                        .worldItemWithoutColliderData.ReturnToPool(child.GetComponent<ItemPickedUp>()
                            .gameObject);
                }
            }
            foreach (Transform child in _poolItemsHoldInPreview)
            {
                var item = child.GetComponent<ItemPickedUp>();
                if (child.GetComponent<ItemPickedUp>() != null && child.GetComponent<ItemPickedUp>().isActiveAndEnabled)
                {
                    child.GetComponent<ItemPickedUp>().worldItemData.ReturnToPool(child.GetComponent<ItemPickedUp>().gameObject);
                    _previewHolder.ItemCount = 0;
                }
            }
        }
    }
}