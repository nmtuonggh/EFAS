using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBorneState : PlayerBaseState
{
    public AirBorneState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
       
    }

    public override void OnUpdateState()
    {
       WalkHandler();
    }

    protected override void OnExitState()
    {
       
    }

    public override void CheckSwitchState()
    {
    }
    
    private void WalkHandler()
    {
        if (InputManager.Instance.Move != Vector2.zero)
        {
            var targetRotation =
                Mathf.Atan2(InputManager.Instance.Move.x, InputManager.Instance.Move.y) * Mathf.Rad2Deg +
                CameraController.Instance.MainCamera.transform.eulerAngles.y;
            var targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

            CameraController.Instance.PlayerRotationObj.transform.rotation = Quaternion.Slerp(
                CameraController.Instance.PlayerRotationObj.transform.rotation,
                targetRotationQuaternion, Time.deltaTime * 10);
            var targetDir = targetRotationQuaternion * Vector3.forward;
            _context.Character.SetMovementDirection(targetDir);
        }
    }
}
