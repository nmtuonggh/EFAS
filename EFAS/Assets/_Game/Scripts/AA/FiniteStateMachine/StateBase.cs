using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public enum StateStatus
{
    None,
    Success,
    Running,
    Failure
}
public class StateBase : ScriptableObject
{
    protected FiniteStateMachine _fsm;
    protected BlackBoard _blackBoard;
    protected bool _isAIControlled;
    public bool canTransiotionToSelf = false;
    protected float elapsedTime { get; private set; }
    [SerializeField] protected ClipTransition _mainAnimation;
    protected AnimancerState _state;

    public virtual void InitState(FiniteStateMachine fsm, BlackBoard blackboard, bool isAIcontrolled)
    {
        //Init
        _fsm = fsm;
        _blackBoard = blackboard;
        _isAIControlled = isAIcontrolled;
        elapsedTime = 0;
    }

    public virtual void EnterState()
    {
        elapsedTime = 0;
        if (_mainAnimation.Clip != null)
        {
            _state = _blackBoard.animancer.Play(_mainAnimation);
        }
    }
    
    public virtual void ConsistentUpdateState()
    {
        elapsedTime += Time.deltaTime;
    }
    
    public virtual StateStatus UpdateState()
    {
        return StateStatus.Running;
    }
    
    public virtual void FixedUpdateState() { }
    
    public virtual void ExitState() { }
  
}
