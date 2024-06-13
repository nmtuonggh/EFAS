using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirBorneState
{
    public JumpState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimJump);
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= 0.1f && PlayerController.Instance.IsGround())
        {
            JumpHandler();
        }
        base.OnUpdateState();
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimJump);
    }

    public override void CheckSwitchState()
    {
        //to fall
        if (_elapsedTime >= 0.3f && !PlayerController.Instance.JumpState())
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