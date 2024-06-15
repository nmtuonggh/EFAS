using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBorneState : PlayerBaseState
{
    public AirBorneState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
       
    }

    public override void OnUpdateState()
    {
       WalkHandler();
    }

    protected override void OnExitState()
    {
       
    }

    public override void CheckSwitchState()
    {
    }
    
    private void WalkHandler()
    {
        if (InputManager.Instance.Move != Vector2.zero)
        {
            InputManager.Instance.MoveHandler();
        }
    }
}
