using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/World Item")]
public class WorldItemData : ScriptableObject, IFactoryWorldItem
{
    public int ID;
    public GameObject WorldItemPrefab;
    public Queue<GameObject> PoolWorldItemData = new Queue<GameObject>(); 

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        if (PoolWorldItemData.Count > 0)
        {
            GameObject item = PoolWorldItemData.Dequeue();
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.transform.SetParent(parent);
            item.gameObject.SetActive(true);
            return item;
        }
        else
        {
            return Instantiate(WorldItemPrefab, position, rotation, parent);
        }
    }
    
    public void ReturnToPool(GameObject item)
    {
        PoolWorldItemData.Enqueue(item);
        item.gameObject.SetActive(false);
    }
}

