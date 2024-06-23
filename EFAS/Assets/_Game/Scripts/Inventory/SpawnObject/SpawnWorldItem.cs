using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorldItem : MonoBehaviour
{
    [SerializeField] private WorldItemData WorldItemData;
    public void SpawnItem()
    {
        Instantiate(WorldItemData.WorldItem, transform.position, Quaternion.identity);
    }
}
