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
        _context.Animator.SetTrigger(Constan.AnimFall);
    }

    public override void OnUpdateState()
    {
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimFall);
    }

    public override void CheckSwitchState()
    {
        //to idle
        if (PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Idle());
        }
        //to walk
        if (InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }
        //to run
        if (InputManager.Instance.runBtnDown && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Run());
        }
    }
}
