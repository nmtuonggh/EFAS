using _Game.Scripts.Cooking;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Inventory.Action
{
    public class ThrowToPot : MonoBehaviour
    {
        [SerializeField] private Transform _poolItemsHoldInPlayer;
        [SerializeField] private Transform _poolItemsHoldInPreview;
        [SerializeField] private PreviewHolder _previewHolder;
        [SerializeField] private Pot _pot;

        public void ThrowItemHoldToPot()
        {
            foreach (Transform child in _poolItemsHoldInPlayer)
            {
                if (child.GetComponent<ItemPickedUp>()!= null && child.GetComponent<ItemPickedUp>().isActiveAndEnabled)
                {
                    
                    child.DOJump((_pot.transform.position + Vector3.up), 2f, 1, 1).OnComplete(() =>
                    {
                        child.tag = "Untagged";
                        _pot.Ingredients.Add(child.GetComponent<ItemPickedUp>().itemData);
                        child.GetComponent<ItemPickedUp>().itemData
                            .ReturnToPool(child.GetComponent<ItemPickedUp>().gameObject);
                    });
                }
            }

            foreach (Transform child in _poolItemsHoldInPreview)
            {
                var item = child.GetComponent<ItemPickedUp>();
                if (item != null && item.isActiveAndEnabled)
                {
                    item.tag = "Untagged";
                    item.GetComponent<Rigidbody>().isKinematic = false;
                    item.itemData.ReturnToPool(item.gameObject);
                    _previewHolder.ItemCount = 0;
                }
            }
        }
    }
}