using System.Collections.Generic;
using _Game.Scripts.Inventory.SpawnObject.Factory;
using UnityEngine;


public enum Type
{
    Ingredients,
    CookedFood
}
[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject, IFactoryItemData
{
    public int ID;
    public Type ItemType;
    public string DisplayName;
    [TextArea(4, 4)] public string Description;
    public Sprite Icon;
    public int MaxStackItem;
    public GameObject ItemPrefab;
    public List<ItemData> Ingredients;
    public Queue<GameObject> PoolItemData = new Queue<GameObject>();

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        if (PoolItemData.Count > 0)
        {
            GameObject item = PoolItemData.Dequeue();
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.transform.SetParent(parent);
            item.gameObject.SetActive(true);
            return item;
        }
        else
        {
            return Instantiate(ItemPrefab, position, rotation, parent);
        }
    }

    public void ReturnToPool(GameObject item)
    {
        PoolItemData.Enqueue(item);
        item.gameObject.SetActive(false);
    }
}