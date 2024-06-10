using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerBaseState
{
    public SlideState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }
    public override void OnEnterState()
    {
        
    }

    public override void OnUpdateState()
    {
        CheckSwitchState();
    }

    public override void OnExitState()
    {
    }

    public override void CheckSwitchState()
    {
        
    }
}
