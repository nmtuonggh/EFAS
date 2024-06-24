using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnWorldItem : MonoBehaviour
{
   public List<WorldItemData> WorldItemDataList;
   [FormerlySerializedAs("parent")] [SerializeField] Transform _parent;
   [FormerlySerializedAs("spawnPos")] [SerializeField] GameObject _spawnPos;
   [FormerlySerializedAs("dropPos")] [SerializeField] GameObject _dropPos;
   [SerializeField] PreviewHolder _previewHolder;

   public void SpawnItem()
   {
      var randomIndex = Random.Range(0, WorldItemDataList.Count);
      WorldItemDataList[randomIndex].Spawn(_spawnPos.transform.position, Quaternion.identity, _parent);
   }

   public void SpawnDropItem(int id)
   {
      foreach (var prefab in WorldItemDataList)
      {
         if(prefab.ID == id)
            prefab.Spawn(_dropPos.transform.position, Quaternion.identity, _parent);
      }
   }

   public void SpawnToPreview(int id, int slotIndex)
   {
      foreach (var prefab in WorldItemDataList)
      {
         if(prefab.ID == id)
         {
               prefab.Spawn(_previewHolder.PreviewSpawnPos[_previewHolder.ItemCount].position, Quaternion.identity, _previewHolder.PreviewSpawnPool);
               //_previewHolder.ItemCount+=1;
         }
      }
   }
}
