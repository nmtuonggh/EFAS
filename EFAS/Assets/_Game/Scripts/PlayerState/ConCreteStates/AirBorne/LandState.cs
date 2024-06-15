using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : PlayerBaseState
{
    public LandState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        Debug.Log("enter land");

        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimLand);
    }

    public override void OnUpdateState()
    {
        Debug.Log("update land");
        _elapsedTime += Time.deltaTime;
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        Debug.Log("exit land");

        _context.Animator.ResetTrigger(Constan.AnimLand);
    }

    public override void CheckSwitchState()
    {
        //to idle
        if (_context.Character.IsGrounded() && !InputManager.Instance.IsMoving()  && _elapsedTime >= 1.2f )
        {
            Debug.Log("Land to idle");
            SwitchState(_factory.Idle());
        }

        //to start walk
        if (_elapsedTime >= 1.2f && InputManager.Instance.IsMoving() &&  _context.Character.IsGrounded())
        {
            SwitchState(_factory.StartWalk());
        }
        
        //to jump
        if (_elapsedTime >= 1.2f && InputManager.Instance.jumpBtn &&  _context.Character.IsGrounded())
        {
            SwitchState(_factory.Jump());
        }
    }
}
