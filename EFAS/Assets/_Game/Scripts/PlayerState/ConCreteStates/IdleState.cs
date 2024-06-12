using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _context.Animator.SetTrigger(Constan.AnimIdle);
    }

    public override void OnUpdateState()
    {
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimIdle);
    }

    public override void CheckSwitchState()
    {
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }

        //to walk
        if (InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }

        //to jump
        if (InputManager.Instance.jumpBtn && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Jump());
        }
    }
}