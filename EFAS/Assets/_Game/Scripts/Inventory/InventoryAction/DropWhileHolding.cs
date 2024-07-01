using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Inventory.Action
{
    public class DropWhileHolding : MonoBehaviour
    {
        [SerializeField] private Transform _poolItemsHoldInPlayer;
        [SerializeField] private Transform _poolItemsHoldInPreview;
        [SerializeField] private BlackBoardInventory _blackBoardInventory;
        [SerializeField] private PreviewHolder _previewHolder;
        [SerializeField] private GameObject[] CubeHold;
        [SerializeField] private Transform _worldItemHolder;

        public void DropItemHold()
        {
            for (int i = _poolItemsHoldInPlayer.childCount - 1; i >= 0; i--)
            {
                Transform child = _poolItemsHoldInPlayer.GetChild(i);
                var item = child.GetComponent<ItemPickedUp>();
                if (item != null)
                {
                    Debug.Log("run");
                    item.tag = "Untagged";
                    item.GetComponent<Rigidbody>().isKinematic = false;
                    item.transform.SetParent(_worldItemHolder);
                }
            }
            foreach (Transform child in _poolItemsHoldInPreview)
            {
                var item = child.GetComponent<ItemPickedUp>();
                if (item != null && item.isActiveAndEnabled)
                {
                    item.worldItemData.ReturnToPool(item.gameObject);
                    _previewHolder.ItemCount = 0;
                }
            }
        }
    }
}