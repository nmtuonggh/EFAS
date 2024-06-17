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
        _context.Character.Jump();
        _context.Animator.SetTrigger(Constan.AnimJump);
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        base.OnUpdateState();
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        base.OnExitState();
        _context.Character.StopJumping();
        _context.Animator.ResetTrigger(Constan.AnimJump);
    }

    public override void CheckSwitchState()
    {
        //to fall
        if (_context.Character.IsFalling() && !_context.Character.IsGrounded())
        {
            SwitchState(_factory.Fall());
        }
        
        // //to idle
        // if(_context.Character.IsGrounded() && !InputManager.Instance.IsMoving())
        // {
        //     SwitchState(_factory.Idle());
        // }
    }
}