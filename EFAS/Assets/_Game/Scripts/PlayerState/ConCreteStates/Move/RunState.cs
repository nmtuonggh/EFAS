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
        _context.Character.Sprint();
    }

    public override void OnUpdateState()
    {
        InputManager.Instance.MoveHandler();
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimRun);
        _context.Character.StopSprinting();
    }

    public override void CheckSwitchState()
    {
        //to fall
        if (_context.Character.IsFalling())
        {
            SwitchState(_factory.Fall());
        }

        //to stop Walk (idle)
        if (InputManager.Instance.runBtnDown && _context.Character.IsGrounded() &&
            !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.StopWalk());
        }

        //to walk
        if (InputManager.Instance.runBtnUp && InputManager.Instance.IsMoving() && _context.Character.IsGrounded())
        {
            SwitchState(_factory.Walk());
        }

        //to jump
        if (_context.Character.IsGrounded() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }
    }
}