
using UnityEngine;

public abstract class PlayerBaseState
{
    protected StatesMachineController _context;
    private FactoryStates _factory;
    //protected Animator _animator;
    public PlayerBaseState(StatesMachineController currentContext, FactoryStates playerFactoryState)
    {
        _context = currentContext;
        _factory = playerFactoryState;
    }
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnExitState();
    public abstract void CheckSwitchState();

    protected void SwitchState(PlayerBaseState newState)
    {
        OnEnterState();
        newState.OnEnterState();
        _context.CurrentState = newState;
    }
}
