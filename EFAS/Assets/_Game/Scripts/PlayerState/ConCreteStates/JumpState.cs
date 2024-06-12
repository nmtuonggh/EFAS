using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirBorneState
{
    private float _jumpToFallThreshold = 0 ;
    public JumpState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _jumpToFallThreshold = 0;
        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimJump);
        JumpHandler();
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        _jumpToFallThreshold += Time.deltaTime;
        base.OnUpdateState();
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimJump);
    }

    public override void CheckSwitchState()
    {
        //to fall
        if (!PlayerController.Instance.JumpState())
        {
            SwitchState(_factory.Fall());
        }
        
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }
    }

    private void JumpHandler()
    {
        var vector3 = PlayerController.Instance.VerticalVelocity;
        vector3.y = Mathf.Sqrt((PlayerController.Instance.JumpHeight * 10) * -2f * PlayerController.Instance.Gravity);
        PlayerController.Instance.VerticalVelocity = vector3;
    }
}