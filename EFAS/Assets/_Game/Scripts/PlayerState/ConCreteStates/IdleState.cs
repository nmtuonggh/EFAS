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
        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimIdle);
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimIdle);
    }

    public override void CheckSwitchState()
    {
        //to walk
        if (InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }
        //to jump
        if(InputManager.Instance.jumpBtn && PlayerController.Instance.IsGround() )
        {
            SwitchState(_factory.Jump());
        }
    }
    
}