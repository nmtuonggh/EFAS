using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryWorldItemWithoutCollider
{
    public GameObject SpawnItemWithoutCollider(Vector3 position, Quaternion rotation, Transform parent);
}
