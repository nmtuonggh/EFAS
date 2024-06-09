using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {}
    public override void OnEnterState()
    {
        Debug.Log("Enter Idle State");
    }

    public override void OnUpdateState()
    {
    }

    public override void OnExitState()
    {
        Debug.Log("Exit Idle State  ");
    }

    public override void CheckSwitchState()
    {
    }
}
