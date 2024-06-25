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
    protected CarryStateMachine _csm;
    protected BlackBoard _blackBoard;
    protected bool _isAIControlled;
    public bool canTransiotionToSelf = false;
    protected float elapsedTime { get; private set; }
    [SerializeField] protected ClipTransition _mainAnimation;
    protected AnimancerState _state;
    protected AnimancerLayer _baseLayer;
    protected AnimancerLayer _carryLayer;

    public virtual void InitState(CarryStateMachine csm, FiniteStateMachine fsm, BlackBoard blackboard, bool isAIcontrolled)
    {
        //Init
        _csm = csm;
        _fsm = fsm;
        _blackBoard = blackboard;
        _isAIControlled = isAIcontrolled;
        elapsedTime = 0;
        _baseLayer = _blackBoard.animancer.Layers[0];
        _carryLayer = _blackBoard.animancer.Layers[1];
        _carryLayer.SetMask(_blackBoard._carryMask);
    }

    public virtual void EnterState()
    {
        elapsedTime = 0;
        if (_mainAnimation.Clip != null)
        {
            //_state = _blackBoard.animancer.Play(_mainAnimation);
            _state = _baseLayer.Play(_mainAnimation);
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
