
using EasyCharacterMovement;
using UnityEngine;
public abstract class PlayerBaseState 
{
    //protected Character _character;
    protected StatesMachineController _context;
    protected FactoryStates _factory;
    protected float _elapsedTime = 0f;

    protected PlayerBaseState(StatesMachineController currentContext, FactoryStates playerFactoryState)
    {
        _context = currentContext;
        _factory = playerFactoryState;
    }
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    protected abstract void OnExitState();
    public abstract void CheckSwitchState();

    protected void SwitchState(PlayerBaseState newState)
    {
        OnExitState();
        newState.OnEnterState();
        _context.CurrentState = newState;
    }
}
