using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/World Item Without Collider")]
public class WorldItemWithoutColliderData : ScriptableObject, IFactoryWorldItemWithoutCollider
{
    public int ID;
    public GameObject ItemPrefab;
    public Queue<GameObject> PoolWorldItemWithoutData = new Queue<GameObject>();
    
    public GameObject SpawnItemWithoutCollider(Vector3 position, Quaternion rotation, Transform parent)
    {
        if (PoolWorldItemWithoutData.Count > 0)
        {
            GameObject item = PoolWorldItemWithoutData.Dequeue();
            item.transform.position = position;
            item.transform.rotation = ItemPrefab.transform.rotation;
            item.transform.SetParent(parent);
            item.gameObject.SetActive(true);
            return item;
        }
        else
        {
            return Instantiate(ItemPrefab, position, ItemPrefab.transform.rotation, parent);
        }
    }
    
    public void ReturnToPool(GameObject item)
    {
        PoolWorldItemWithoutData.Enqueue(item);
        item.gameObject.SetActive(false);
    }
}