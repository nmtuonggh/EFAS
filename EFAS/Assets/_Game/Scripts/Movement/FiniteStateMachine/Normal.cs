using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/States/Normal")]
public class Normal : StateBase
{
    [SerializeField] private CarryState _carryState;
    public override void EnterState()
    {
        _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
    }
    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }
        
        if(_blackBoard.PreviewHolder.ItemCount > 0)
        {
            _csm.ChangeState(_carryState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }
}
