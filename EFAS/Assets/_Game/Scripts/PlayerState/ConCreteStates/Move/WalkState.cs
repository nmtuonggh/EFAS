using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerBaseState
{
    public WalkState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        //_context.Animator.SetTrigger(Constan.AnimWalk);
    }

    public override void OnUpdateState()
    {
        InputManager.Instance.MoveHandler();
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        //_context.Animator.ResetTrigger(Constan.AnimWalk);
    }

    public override void CheckSwitchState()
    {
        //to jump
        if (_context.Character.IsGrounded() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }

        //to run
        if (InputManager.Instance.IsMoving() && InputManager.Instance.runBtnDown && _context.Character.IsGrounded())
        {
            SwitchState(_factory.Run());
        }

        //to stop walk
        if (!InputManager.Instance.IsMoving() && _context.Character.IsGrounded())
        {
            SwitchState(_factory.StopWalk());
        }

        //to fall
        if (!_context.Character.IsFalling() && !_context.Character.IsGrounded())
        {
            SwitchState(_factory.Fall());
        }
    }
}