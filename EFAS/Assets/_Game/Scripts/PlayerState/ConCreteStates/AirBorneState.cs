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

    public override void OnExitState()
    {
       
    }

    public override void CheckSwitchState()
    {
    }
    
    private static void WalkHandler()
    {
        if (InputManager.Instance.Move != Vector2.zero)
        {
            var targetRotation =
                Mathf.Atan2(InputManager.Instance.Move.x, InputManager.Instance.Move.y) * Mathf.Rad2Deg +
                PlayerController.Instance.MainCamera.transform.eulerAngles.y;
            var targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

            PlayerController.Instance.PlayerRotationObj.transform.rotation = Quaternion.Slerp(
                PlayerController.Instance.PlayerRotationObj.transform.rotation,
                targetRotationQuaternion, Time.deltaTime * PlayerController.Instance.SmoothRotation);
            var targetDir = targetRotationQuaternion * Vector3.forward;

            PlayerController.Instance.CharacterController.Move(
                targetDir.normalized * (PlayerController.Instance.Speed * Time.deltaTime) +
                new Vector3(0.0f, PlayerController.Instance.VerticalVelocity.y, 0.0f) * Time.deltaTime); 
        }
    }
}
