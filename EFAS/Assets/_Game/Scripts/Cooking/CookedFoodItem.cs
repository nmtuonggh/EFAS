using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Cooking
{
    [CreateAssetMenu(fileName = "CookedFoodItem", menuName = "Cooking/CookedFoodItem")]
    public class CookedFoodItem : ScriptableObject, IFactoryCookedFood
    {
        public int ID;
        public string DisplayName;
        [TextArea(4,4)]
        public string Description;
        public Sprite Icon;
        public int MaxStackItem;
        public GameObject CookedFoodPrefab;
        public List<ItemData> Ingredients;
        public float Price;
        public Queue<GameObject> PoolCookedFoodData = new Queue<GameObject>();
        
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (PoolCookedFoodData.Count > 0)
            {
                GameObject item = PoolCookedFoodData.Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.transform.SetParent(parent);
                item.gameObject.SetActive(true);
                return item;
            }
            else
            {
                return Instantiate(CookedFoodPrefab, position, rotation, parent);
            }
        }
        
        public void ReturnToPool(GameObject item)
        {
            PoolCookedFoodData.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }
}