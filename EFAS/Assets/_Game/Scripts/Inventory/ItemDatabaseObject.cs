using System.Collections.Generic;
using _Game.Scripts.Inventory.Item.Scripts;
using UnityEngine;

namespace _Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory System/Items/Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public BaseItemObject[] Items;
        public Dictionary<BaseItemObject, int> GetId = new Dictionary<BaseItemObject, int>();
        public Dictionary<int, BaseItemObject> GetItem = new Dictionary<int, BaseItemObject>();
    
        public void OnAfterDeserialize()
        {
            GetItem = new Dictionary<int, BaseItemObject>();
            GetId = new Dictionary<BaseItemObject, int>();
            for (int i = 0; i < Items.Length; i++)
            {
                GetId.Add(Items[i], i);
                GetItem.Add(i, Items[i]);
            }
        }
    
        public void OnBeforeSerialize()
        {
        }
    }
}
