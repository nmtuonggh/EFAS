using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/Carry")]
public class CarryState : StateBase
{
    [SerializeField] private LinearMixerTransition _carryanim;
    [SerializeField] private Normal _normalState;
    
    public override void EnterState()
    {
        base.EnterState();
        _state = _carryLayer.Play(_carryanim);

    }

    public override StateStatus UpdateState()
    {
        Debug.Log("On update carry");
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }
        ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, _blackBoard.PreviewHolder.ItemCount, 55 * Time.deltaTime);
        
        if(_blackBoard.PreviewHolder.ItemCount == 0)
        {
            _carryLayer.StartFade(0, 0.1f);
            _csm.ChangeState(_normalState);
            return StateStatus.Success;
        }
        return StateStatus.Running;
    }
    
}