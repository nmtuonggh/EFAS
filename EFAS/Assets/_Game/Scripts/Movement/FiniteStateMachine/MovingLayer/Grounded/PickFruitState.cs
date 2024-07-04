using _Game.Scripts.Event;
using UnityEngine;

namespace _Game.Scripts.Movement.FiniteStateMachine.MovingLayer.Grounded
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/PickFruit")]
    public class PickFruitState : GroundedState
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private WalkState _walkState;
        public static event System.Action OnPickFruitEnd;
        public GameEventT<float> DecreaseStrengthPickFruit;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.UIManager.buttonInventory.SetActive(false);
            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_idleState);
                OnPickFruitEnd?.Invoke();
                DecreaseStrengthPickFruit?.Raise(0.05f);
                _blackBoard.isPicking = false;
                _blackBoard.UIManager.buttonInventory.SetActive(true);
            };
        }
        
        private void RotatePlayer()
        {
            Vector3 directionToTree = _blackBoard.PickFruitRange.TargetTree.position - _fsm.transform.position;
            directionToTree.y = 0;
            _blackBoard.playerMovement.RotateTowardsWithSlerp(directionToTree, true);
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