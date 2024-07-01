using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnWorldItem : MonoBehaviour
{
   public List<WorldItemData> WorldItemDataList;
   public List<WorldItemWithoutColliderData> WorldItemDataWithoutColliderList;
   
   [FormerlySerializedAs("_parent")] [FormerlySerializedAs("parent")] [SerializeField] Transform _parentSpawnItem;
   [FormerlySerializedAs("spawnPos")] [SerializeField] GameObject _spawnPos;
   [FormerlySerializedAs("dropPos")] [SerializeField] GameObject _dropPos;
   [SerializeField] PreviewHolder _previewHolder;

   [SerializeField] private List<Transform> _playerHoldPos;
   [SerializeField] private Transform _playerHoldPool;

   public void SpawnItem()
   {
      var randomIndex = Random.Range(0, WorldItemDataList.Count);
      WorldItemDataList[randomIndex].Spawn(_spawnPos.transform.position, Quaternion.identity, _parentSpawnItem);
   }

   public void SpawnDropItem(int id)
   {
      foreach (var prefab in WorldItemDataList) 
      {
         if(prefab.ID == id)
            prefab.Spawn(_dropPos.transform.position, Quaternion.identity, _parentSpawnItem);
      }
   }

   public void SpawnToPreview(int id, int slotIndex)
   {
      foreach (var prefab in WorldItemDataList)
      {
         if(prefab.ID == id)
         {
               prefab.Spawn(_previewHolder.PreviewSpawnPos[_previewHolder.ItemCount].position, Quaternion.identity, _previewHolder.PreviewSpawnPool);
         }
      }
   }

   public void SpawnToPlayer(int id, int slotIndex)
   {
      foreach (var prefab in WorldItemDataWithoutColliderList)
      {
         if(prefab.ID == id)
         {
            prefab.SpawnItemWithoutCollider(_playerHoldPos[_previewHolder.ItemCount].position, Quaternion.identity, _playerHoldPool);
         }
      }
   }
}
