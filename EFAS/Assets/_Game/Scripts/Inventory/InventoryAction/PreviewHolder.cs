using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewHolder : MonoBehaviour
{
    [SerializeField] private List<Transform> _previewSpawnPos;
    [SerializeField] private int _itemCount = 0;
    [SerializeField] private Transform _previewSpawnPool;
    public event Action OnHoldingItem;
    public List<Transform> PreviewSpawnPos
    {
        get => _previewSpawnPos;
        set => _previewSpawnPos = value;
    }

    public int ItemCount
    {
        get => _itemCount;
        set => _itemCount = value;
    }

    public Transform PreviewSpawnPool
    {
        get => _previewSpawnPool;
        set => _previewSpawnPool = value;
    }
    
    
}
