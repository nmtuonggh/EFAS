using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryWorldItem
{
    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent);
}

