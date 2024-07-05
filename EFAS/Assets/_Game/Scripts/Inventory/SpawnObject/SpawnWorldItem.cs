using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Cooking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using DG.Tweening;

public class SpawnWorldItem : MonoBehaviour
{
   public List<ItemData> ItemDataList;
   //public List<WorldItemData> WorldItemDataList;

   [FormerlySerializedAs("_parent")] [FormerlySerializedAs("parent")] [SerializeField] Transform _parentSpawnItem;
   [FormerlySerializedAs("spawnPos")] [SerializeField] GameObject _spawnPos;
   [FormerlySerializedAs("dropPos")] [SerializeField] GameObject _dropPos;
   [SerializeField] GameObject _cookedFoodSpawnPos;
   [SerializeField] GameObject tweencookedFoodSpawnPos;
   [SerializeField] PreviewHolder _previewHolder;

   [SerializeField] private List<Transform> _playerHoldPos;
   [SerializeField] private Transform _playerHoldPool;

   public void SpawnItem()
   {
      var randomIndex = Random.Range(0, ItemDataList.Count);
      ItemDataList[randomIndex].Spawn(_spawnPos.transform.position, Quaternion.identity, _parentSpawnItem);
   }

   public void SpawnDropItem(int id)
   {
      foreach (var prefab in ItemDataList) 
      {
         if(prefab.ID == id)
            prefab.Spawn(_dropPos.transform.position, Quaternion.identity, _parentSpawnItem);
      }
   }

   public void SpawnToPreview(int id, int slotIndex)
   {
      foreach (var prefab in ItemDataList)
      {
         if(prefab.ID == id)
         {
               prefab.Spawn(_previewHolder.PreviewSpawnPos[_previewHolder.ItemCount].position, Quaternion.identity, _previewHolder.PreviewSpawnPool);
         }
      }
   }

   public void SpawnToPlayer(int id, int slotIndex)
   {
      foreach (var prefab in ItemDataList)
      {
         if(prefab.ID == id)
         {
            prefab.Spawn(_playerHoldPos[_previewHolder.ItemCount].position, prefab.ItemPrefab.transform.rotation, _playerHoldPool);
         }
      }
   }
   
   public void SpawnCookedFoodItem(int id)
   {
      foreach (var prefab in ItemDataList)
      {
         if(prefab.ID == id)
         {
            var item = prefab.Spawn(_cookedFoodSpawnPos.transform.position, Quaternion.identity, _parentSpawnItem);
            item.transform.DOJump(tweencookedFoodSpawnPos.transform.position, 1, 1,1);
         }
      }
   }
   
}
