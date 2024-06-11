using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public RunState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _context.Animator.SetTrigger(Constan.AnimRun);
        PlayerController.Instance.Speed += 10f;
    }

    public override void OnUpdateState()
    {
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimRun);
        PlayerController.Instance.Speed = PlayerController.Instance.NormalSpeed;
    }

    public override void CheckSwitchState()
    {
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }

        //to idle
        if ((InputManager.Instance.runBtnDown) && PlayerController.Instance.IsGround() &&
            !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Idle());
        }

        //to walk
        if (InputManager.Instance.runBtnUp && InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }

        //to jump
        if (PlayerController.Instance.IsGround() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }
    }
}