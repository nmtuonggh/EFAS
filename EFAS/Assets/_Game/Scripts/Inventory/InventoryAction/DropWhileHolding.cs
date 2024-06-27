using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class DropWhileHolding : MonoBehaviour
    {
        [SerializeField] private List<ItemPickedUp> itemsHoldInPlayer;
        [SerializeField] private List<ItemPickedUp> itemsHoldInPreview;
        [SerializeField] private Transform _poolItemsHoldInPlayer;
        [SerializeField] private Transform _poolItemsHoldInPreview;
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        [SerializeField] private PreviewHolder _previewHolder;

        private void Start()
        {
        }

        private void GetChild()
        {
            itemsHoldInPlayer = new List<ItemPickedUp>();
            itemsHoldInPreview = new List<ItemPickedUp>();
            foreach (Transform child in _poolItemsHoldInPlayer)
            {
                if (child.GetComponent<ItemPickedUp>() != null && child.GetComponent<ItemPickedUp>().isActiveAndEnabled)
                {
                    child.GetComponent<ItemPickedUp>()
                        .worldItemWithoutColliderData.ReturnToPool(child.GetComponent<ItemPickedUp>()
                            .gameObject);
                    _blackBoardInventory.spawnWorldItem.SpawnDropItem(child.GetComponent<ItemPickedUp>().worldItemWithoutColliderData.ID);
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

        public void DropItemHold()
        {
            GetChild();
        }
    }
}