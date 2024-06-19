using UnityEngine;

namespace _Game.Scripts.Inventory.Item.Scripts
{
    [CreateAssetMenu(fileName = "FruitObject", menuName = "Inventory System/Items/Fruit")]
    public class FruitObject : BaseItemObject
    {
        public void Awake()
        {
            type = ItemType.Fruit;
        }
    }
}
