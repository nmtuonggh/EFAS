using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AirBorneState
{
    public FallState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        Debug.Log("Start FallState");
        _context.Animator.SetTrigger(Constan.AnimFall);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        Debug.Log("Exit FallState");
        _context.Animator.ResetTrigger(Constan.AnimFall);
    }

    public override void CheckSwitchState()
    {
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }

        //to idle
        if (PlayerController.Instance.IsGround() && !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Idle());
        }

        //to walk
        if (InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }
        //to run
    }
}