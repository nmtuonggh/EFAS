using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStateMachine
{
    void OnEnter(PlayerController player);
    void OnExecute(PlayerController player);
    void OnExit(PlayerController player);
}
