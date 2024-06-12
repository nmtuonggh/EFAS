using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public RunState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        _context.Animator.SetTrigger(Constan.AnimRun);
        PlayerController.Instance.Speed += 10f;
    }

    public override void OnUpdateState()
    {
        MoveHandler();
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimRun);
        PlayerController.Instance.Speed = PlayerController.Instance.NormalSpeed;
    }

    public override void CheckSwitchState()
    {
        //to fall
        if (!PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Fall());
        }
        
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }

        //to idle
        if ((InputManager.Instance.runBtnDown) && PlayerController.Instance.IsGround() &&
            !InputManager.Instance.IsMoving())
        {
            SwitchState(_factory.Idle());
        }

        //to walk
        if (InputManager.Instance.runBtnUp && InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Walk());
        }

        //to jump
        if (PlayerController.Instance.IsGround() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }
    }
    private static void MoveHandler()
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