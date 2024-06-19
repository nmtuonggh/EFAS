using UnityEngine;

namespace _Game.Scripts.Inventory.Item.Scripts
{
    public enum ItemType
    {
        Food,
        Weapon,
        Dish,
        Default
    }
    public class BaseItemObject : ScriptableObject
    {
        public GameObject prefab;
        public ItemType type;
        public bool stackable;
        [TextArea(15, 20)]
        public string description;
    }
}
