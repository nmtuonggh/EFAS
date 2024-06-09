using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMachineController : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private FactoryStates _states;
    [SerializeField] public Animator _animator;

    //get,set
    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    private void Awake()
    {
        _states = new FactoryStates(this);
        _currentState = _states.Walk();
        _currentState.OnEnterState();
    }

    private void Update()
    {
        _currentState.OnUpdateState();
    }
}
