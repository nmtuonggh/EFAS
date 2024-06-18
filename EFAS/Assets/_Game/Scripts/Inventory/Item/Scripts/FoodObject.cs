using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Inventory.Item.Scripts;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : BaseItemObject
{
    public void Awake()
    {
        type = ItemType.Food;
    }
}
