using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerBaseState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
}
