using _Game.Scripts.Cooking;
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
                if (child.GetComponent<ItemPickedUp>()!= null && isActiveAndEnabled)
                {
                    _pot.Ingredients.Add(child.GetComponent<ItemPickedUp>().itemData);
                    child.GetComponent<ItemPickedUp>().itemData.ReturnToPool(child.GetComponent<ItemPickedUp>().gameObject);
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