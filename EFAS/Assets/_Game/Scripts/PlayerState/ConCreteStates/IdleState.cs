using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    float delayJumpTime = 0.4f;
    public IdleState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _elapsedTime = 0;
        _context.Animator.SetBool(Constan.AnimIdle, true);
    }

    public override void OnUpdateState()
    {
        _elapsedTime += Time.deltaTime;
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.SetBool(Constan.AnimIdle, false);
    }

    public override void CheckSwitchState()
    {
        if (InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }
        if(InputManager.Instance.jumpBtn && PlayerController.Instance.IsGround() )
        {
            SwitchState(_factory.Jump());
        }
    }
    
}