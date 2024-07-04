using _Game.Scripts.Event;
using UnityEngine;

namespace _Game.Scripts.Movement.FiniteStateMachine.MovingLayer.Grounded
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Fishing")]
    public class FishingState : GroundedState
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private WalkState _walkState;
        public static event System.Action OnFishingEnd;
        public GameEventT<float> DecreaseStrength;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.UIManager.buttonInventory.SetActive(false);
            _blackBoard.UIManager.Rod.SetActive(true);
            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_idleState);
                OnFishingEnd?.Invoke();
                DecreaseStrength.Raise(0.05f);
                _blackBoard.isFishing = false;
                _blackBoard.UIManager.buttonInventory.SetActive(true);
                _blackBoard.UIManager.Rod.SetActive(false);
            };
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            Vector3 directionToFishingSpot = _blackBoard.fishingTarget.position - _fsm.transform.position;
            directionToFishingSpot.y = 0;
            _blackBoard.playerMovement.RotateTowardsWithSlerp(directionToFishingSpot, true);
        }

        public override StateStatus UpdateState()
        {
            RotatePlayer();
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
        
        public override void ExitState()
        {
            base.ExitState();

            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}