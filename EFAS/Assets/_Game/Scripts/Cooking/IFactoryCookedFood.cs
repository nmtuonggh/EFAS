using UnityEngine;

namespace _Game.Scripts.Cooking
{
    public interface IFactoryCookedFood
    {
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent);
    }
}