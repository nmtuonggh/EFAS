using UnityEngine;

using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Die")]
public class DieState : StateBase
{
    public static System.Action OnDie;
    public override void EnterState()
    {
        base.EnterState();
        _state.Events.OnEnd = () =>
        {
            OnDie?.Invoke();
            _state.Events.OnEnd = null; 
        };
    }
}