using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWalk : PlayerBaseState
{
    public StartWalk(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _context.Animator.SetTrigger(Constan.AnimStartWalk);
        _elapsedTime = 0;
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimStartWalk);
    }

    public override void CheckSwitchState()
    {
        if (_elapsedTime >= 0.3)
        {
            SwitchState(_factory.Walk());
        }
    }
}
