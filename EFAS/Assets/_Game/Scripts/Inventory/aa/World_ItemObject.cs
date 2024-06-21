using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World_ItemObject", menuName = "Inventory System/Items/World_ItemObject")]
public class World_ItemObject : BaseItemObject
{
    [SerializeField] private GameObject _prefab;
}
