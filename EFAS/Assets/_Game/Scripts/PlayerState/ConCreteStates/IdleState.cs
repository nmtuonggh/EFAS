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
        Debug.Log("enter idle");
        _context.Animator.SetTrigger(Constan.AnimIdle);
        _elapsedTime = 0;
    }

    public override void OnUpdateState()
    {
        Debug.Log("update idle");
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimIdle);
    }

    public override void CheckSwitchState()
    {
        //to start walk
        if (InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.StartWalk());
        }

        //to jump
        if(_context.Character.IsGrounded() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }
    }
}