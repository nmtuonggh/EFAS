using System.Collections.Generic;
using _Game.Scripts.Inventory.Item.Scripts;
using UnityEngine;

namespace _Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory System/Items/Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public BaseItemObject[] Items;
        public Dictionary<int, BaseItemObject> GetItem = new Dictionary<int, BaseItemObject>();
    
        public void OnAfterDeserialize()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].Id = i;
                GetItem.Add(i, Items[i]);
            }
        }
    
        public void OnBeforeSerialize()
        {
            GetItem = new Dictionary<int, BaseItemObject>();
        }
    }
}
