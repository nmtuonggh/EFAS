using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Movement.FiniteStateMachine.MovingLayer.Grounded;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/Idle")]
public class IdleState : GroundedState
{
    [SerializeField] private WalkState _walkState;
    [SerializeField] private FishingState _fishingState;
    [SerializeField] private PickFruitState _pickFruitState;

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
        
        if (_blackBoard.isFishing && _blackBoard.PreviewHolder.ItemCount == 0)
        {
            _fsm.ChangeState(_fishingState);
            return StateStatus.Success;
        }

        if (_blackBoard.isPicking && _blackBoard.PreviewHolder.ItemCount == 0)
        {
            _fsm.ChangeState(_pickFruitState );
            return StateStatus.Success;
        }
        
        if (_blackBoard.moveDirection.magnitude > 0f)
        {
            _fsm.ChangeState(_walkState);
            return StateStatus.Success;
        }

        

        return StateStatus.Running;
    }
}
