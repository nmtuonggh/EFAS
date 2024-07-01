using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/Idle")]
public class IdleState : GroundedState
{
    [SerializeField] private WalkState _walkState;
    [SerializeField] private CarryState _carryState;

    public override void EnterState()
    {
        base.EnterState();
        _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackBoard.moveDirection.magnitude > 0f)
        {
            _fsm.ChangeState(_walkState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }
}
