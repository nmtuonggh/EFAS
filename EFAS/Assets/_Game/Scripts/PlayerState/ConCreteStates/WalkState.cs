using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerBaseState
{
    public WalkState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }
    public override void OnEnterState()
    {
        _context.Animator.SetTrigger(Constan.AnimWalk);
    }

    public override void OnUpdateState()
    {
        Debug.Log(InputManager.Instance.Move.magnitude);
        WalkHandler();
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        //_context.Animator.SetBool(Constan.AnimWalk, false);
    }

    public override void CheckSwitchState()
    {
        //to jump
        if (PlayerController.Instance.IsGround() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }
        //to idle
        if(!InputManager.Instance.IsMoving())
        {
            Debug.Log("Walk ===> Idle");
            SwitchState(_factory.Idle());
        }
        //to run
    }
    
    void WalkHandler()
    {
        float targetRotation =
            Mathf.Atan2(InputManager.Instance.Move.x, InputManager.Instance.Move.y) * Mathf.Rad2Deg +
            PlayerController.Instance.MainCamera.transform.eulerAngles.y;
        Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

        PlayerController.Instance.PlayerRotationObj.transform.rotation = Quaternion.Slerp(PlayerController.Instance.PlayerRotationObj.transform.rotation,
            targetRotationQuaternion, Time.deltaTime * PlayerController.Instance.SmoothRotation);
        Vector3 targetDir = targetRotationQuaternion * Vector3.forward;

        PlayerController.Instance.CharacterController.Move(targetDir.normalized * (PlayerController.Instance.Speed * Time.deltaTime) +
                                  new Vector3(0.0f, PlayerController.Instance.VerticalVelocity.y, 0.0f) * Time.deltaTime);
    }
}

