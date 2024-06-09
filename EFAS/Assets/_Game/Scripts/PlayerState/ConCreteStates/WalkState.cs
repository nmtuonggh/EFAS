using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerBaseState
{
    public WalkState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }
    public override void OnEnterState()
    {
        _context._animator.SetBool("run", true);
    }

    public override void OnUpdateState()
    {
    }

    public override void OnExitState()
    {
    }

    public override void CheckSwitchState()
    {
        
    }
}

