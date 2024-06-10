using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public RunState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }
    public override void OnEnterState()
    {
        _context.Animator.SetBool("run", true);
    }

    public override void OnUpdateState()
    {
        CheckSwitchState();
    }

    public override void OnExitState()
    {
    }

    public override void CheckSwitchState()
    {
        
    }
}
