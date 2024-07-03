using UnityEngine;

namespace _Game.Scripts.Inventory.SpawnObject.Factory
{
    public interface IFactoryItemData
    {
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent);
    }
}