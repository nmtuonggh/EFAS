using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/World Item")]
public class WorldItemData : ScriptableObject, IFactoryWorldItem
{
    public int ID;
    public GameObject WorldItemPrefab;


    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        return Instantiate(WorldItemPrefab, position, rotation, parent);
    }
}

