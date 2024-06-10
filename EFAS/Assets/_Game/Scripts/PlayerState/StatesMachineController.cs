using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMachineController : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private FactoryStates _states;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    //get,set
    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public Animator Animator
    {
        get => _animator;
        set => _animator = value;
    }
    private void Awake()
    {
        _states = new FactoryStates(this);
        _currentState = _states.Idle();
        _currentState.OnEnterState();
    }

    private void Update()
    {
        _currentState.OnUpdateState();
    }
}