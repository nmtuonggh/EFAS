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
        base.OnExitState();
        _context.Animator.ResetTrigger(Constan.AnimFall);
    }

    public override void CheckSwitchState()
    {

        //to land
        if (_context.Character.IsGrounded() && !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Land());
        }
        //to start walk
        if (_context.Character.IsGrounded() && InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.StartWalk());
        }
        //to run   
        if (_context.Character.IsGrounded() && _context.Character.IsSprinting() && InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Run());
        }
    }
}