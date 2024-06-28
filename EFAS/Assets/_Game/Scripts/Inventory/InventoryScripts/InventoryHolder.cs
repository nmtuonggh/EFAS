using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] protected InventorySystem _inventorySystem;
    public InventorySystem InventorySystem => _inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;
    private void Awake()
    {
        _inventorySystem = new InventorySystem(_inventorySize);
    }

    /*[System.Serializable]
    public struct InventorySaveData
    {
        public InventorySystem InvSystem;
        public Vector3 Position;
        public Quaternion Rotation;
        
        public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
        {
            this.InvSystem = _invSystem;
            this.Position = _position;
            this.Rotation = _rotation;
        }
        public InventorySaveData(InventorySystem _invSystem)
        {
            this.InvSystem = _invSystem;
            this.Position = Vector3.zero;
            this.Rotation = Quaternion.identity;
        }
    }*/
}
