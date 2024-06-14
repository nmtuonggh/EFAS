using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWalk : PlayerBaseState
{
    public StopWalk(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimStopWalk);
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimStopWalk);
    }

    public override void CheckSwitchState()
    {
        //to idle
        if(_elapsedTime >= 0.6f &&!InputManager.Instance.IsMoving() && _context.Character.IsGrounded())
        {
            SwitchState(_factory.Idle());
        }
        // //to start walk
        // if (_elapsedTime is > 0.3f and < 0.6f && InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        // {
        //     SwitchState(_factory.StartWalk());
        // }
        // //to jump
        // if (_elapsedTime is > 0.3f and < 0.6f && PlayerController.Instance.IsGround() && InputManager.Instance.jumpBtn)
        // {
        //     SwitchState(_factory.Jump());
        // }
    }
}
