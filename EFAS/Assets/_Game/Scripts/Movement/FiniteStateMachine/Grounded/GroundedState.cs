using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : StateBase
{
    [SerializeField] protected JumpState _jumState;
    [SerializeField] protected FallState _fallState;
    
    public bool canJump = true;

    public override StateStatus UpdateState()
    {
        if (HandelJump())
        {
            _fsm.ChangeState(_jumState);
            return StateStatus.Success;
        }

        if (!_blackBoard.playerMovement.IsGrounded())
        {
            _fsm.ChangeState(_fallState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    protected virtual bool HandelJump()
    {
        if (canJump && _blackBoard.playerMovement.CanJump() && !_blackBoard.InventoryManager.isHolding)
        {
            return _blackBoard.jump;
        }

        return false;
    }
}
