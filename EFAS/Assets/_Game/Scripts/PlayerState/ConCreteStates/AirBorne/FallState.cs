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
        _context.Animator.SetTrigger(Constan.AnimFall);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimFall);
    }

    public override void CheckSwitchState()
    {
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }
        
        //to land
        if (PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Land());
        }
    }
}