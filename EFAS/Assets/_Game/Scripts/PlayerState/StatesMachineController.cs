using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMachineController : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private FactoryStates _states;

    private void Awake()
    {
        _states = new FactoryStates(this);
        _currentState = _states.Idle();
        _currentState.OnEnterState();
    }
}
