using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerBaseState
{
    public FallState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }
    public override void OnEnterState()
    {
        _context.Animator.SetTrigger("fall");
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
        if (PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Idle());
        }
    }
}
