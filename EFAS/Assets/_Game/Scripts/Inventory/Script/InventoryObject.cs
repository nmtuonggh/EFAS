using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using _Game.Scripts.Inventory.Item.Scripts;
using UnityEditor;
using UnityEngine;
namespace _Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject //, ISerializationCallbackReceiver
    {
        public string savePath;
        public Inventory Container;
        //private ItemDatabaseObject database;
        public ItemDatabaseObject database;

        /*private void OnEnable()
        {
#if UNITY_EDITOR
            database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/_Game/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
            database = Resources.Load<ItemDatabaseObject>("Database");
#endif
        }*/

        public void AddItem(Item.Scripts.Item _item, int amount)
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].item.Id == _item.Id)
                {
                    Container.Items[i].AddAmount(amount);
                    return;
                }
            }
            Container.Items.Add(new InventorySlot(_item.Id, _item, amount));
        }

        [ContextMenu("Save")]
        public void Save()
        {
            /*string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            bf.Serialize(file, saveData);   
            file.Close();*/
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();
        }
        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                    Container = (Inventory)formatter.Deserialize(stream);
                    stream.Close();
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error loading data: " + ex.Message);
                    // Xử lý lỗi: Khôi phục hoặc thông báo lỗi đến người dùng
                }
            }
        }

        [ContextMenu("Clear")]
        
        public void Clear()
        {
            Container = new Inventory();
        }
        
        /*public void OnAfterDeserialize()
        {
            for(int i = 0; i < Container.Items.Count; i++)
            {
                Container.Items[i].item = database.GetItem[Container.Items[i].ID];
            } 
        }*/
        
        public void OnBeforeSerialize()
        {
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> Items = new List<InventorySlot>();
        //public InventorySlot[] Items = 
    }

    [System.Serializable]
    public class InventorySlot
    {
        public int ID;
        public Item.Scripts.Item item;
        public int amount;

        public InventorySlot(int _id, Item.Scripts.Item _item, int _amount)
        {
            ID = _id;
            item = _item;
            amount = _amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}