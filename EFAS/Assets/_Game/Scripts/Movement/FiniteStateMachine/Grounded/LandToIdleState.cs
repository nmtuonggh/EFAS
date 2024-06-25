using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/LandToIdle")]

public class LandToIdleState : GroundedState
{
    [SerializeField] private IdleState _idleState;
    public override void EnterState()
    {
        base.EnterState();

        _state.Events.OnEnd = () =>
        {
            _fsm.ChangeState(_idleState);
        };
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        return StateStatus.Running;
    }
}
