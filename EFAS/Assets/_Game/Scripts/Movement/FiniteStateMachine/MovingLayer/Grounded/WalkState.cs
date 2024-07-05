using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using Animancer;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/Walk")]
public class WalkState : GroundedState
{
    [SerializeField] private IdleState _idleState;
    [SerializeField] private SprintState _sprintState;
    [SerializeField] private WalkToIdleState _walkToIdleState;
    [SerializeField] private LinearMixerTransition _walkingBlendTree;
    public GameEventT<float> DecreaseStrength;

    public override void EnterState()
    {
        base.EnterState();
        _blackBoard.isFishing = false;
        _blackBoard.isPicking = false;
        _state = _baseLayer.Play(_walkingBlendTree);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, _blackBoard.playerMovement.GetSpeed(), 55 * Time.deltaTime);
        DecreaseStrength.Raise(0.001f * Time.deltaTime);
        _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);

        if(_blackBoard.sprint && !(_blackBoard.PreviewHolder.ItemCount > 0))
        {
            _fsm.ChangeState(_sprintState);
            return StateStatus.Success;
        }

        if (_blackBoard.moveDirection.magnitude < 0.1f )
        {
            _fsm.ChangeState(_walkToIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }
}
