using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Die")]
public class DieState : StateBase
{
    public static System.Action OnDie;
    public override void EnterState()
    {
        base.EnterState();
        _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
        _state.Events.OnEnd = () =>
        {
            OnDie?.Invoke();
            _state.Events.OnEnd = null; 
        };
    }
}