using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerBaseState
{
    public SlideState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _context.Animator.SetTrigger(Constan.AnimSlide);
    }

    public override void OnUpdateState()
    {
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimSlide);
    }

    public override void CheckSwitchState()
    {
        if (!PlayerController.Instance.IsSliding && PlayerController.Instance.IsGround() &&
            !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Idle());
        }
        //to start walk
        if (!PlayerController.Instance.IsSliding && InputManager.Instance.IsMoving() &&
            PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.StartWalk());
        }
    }
}