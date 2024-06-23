using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnWorldItem : MonoBehaviour
{
   public List<WorldItemData> WorldItemDataList;
   [SerializeField] Transform parent;
   [SerializeField] GameObject spawnPos;

   public void SpawnItem()
   {
      var randomIndex = Random.Range(0, WorldItemDataList.Count);
      WorldItemDataList[randomIndex].Spawn(spawnPos.transform.position, Quaternion.identity, parent);
   }
}
