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
        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimLand);
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        //Debug.Log("exit land");

        _context.Animator.ResetTrigger(Constan.AnimLand);
    }

    public override void CheckSwitchState()
    {
        //to idle
        if (_elapsedTime >= 1.2f && PlayerController.Instance.IsGround() && !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Idle());
        }

        //to walk
        if (_elapsedTime >= 1.2f && InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }
        
        //to jump
        if (_elapsedTime >= 1.2f && InputManager.Instance.jumpBtn && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Jump());
        }
    }
}
