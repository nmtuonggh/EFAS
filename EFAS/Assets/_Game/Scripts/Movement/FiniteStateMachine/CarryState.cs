using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/Carry")]
public class CarryState : StateBase
{
    [SerializeField] private AnimationClip _carryanim;
    [SerializeField] private Normal _normalState;
    public override void EnterState()
    {
        //base.EnterState();
        _carryLayer.Play(_mainAnimation);
    }

    public override StateStatus UpdateState()
    {
        base.UpdateState();
        //_blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);
        if(_blackBoard.PreviewHolder.ItemCount == 0)
        {
            Debug.Log("_blackBoard.PreviewHolder.ItemCount" + _blackBoard.PreviewHolder.ItemCount);
            _csm.ChangeState(_normalState);
            return StateStatus.Success;
        }
        return StateStatus.Running;
    }
    
    public override void ExitState()
    {
        base.ExitState();
    }
}