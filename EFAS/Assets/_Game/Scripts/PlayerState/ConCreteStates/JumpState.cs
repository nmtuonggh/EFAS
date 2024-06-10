using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    private float jumpTime = 0.2f;
    public JumpState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext, playerFactoryState)
    {
    }
    public override void OnEnterState()
    {
        Debug.Log("Enter Jump State");
        _elapsedTime = 0;
        _context.Animator.SetTrigger(Constan.AnimJump);
        JumpHandler();
    }

    public override void OnUpdateState()
    {
        Debug.Log(PlayerController.Instance.VerticalVelocity.y);   

        _elapsedTime+=Time.deltaTime;
        if (!PlayerController.Instance.JumpState())
        {
            _context.Animator.SetTrigger("fall");
        }
        WalkHandler();
        CheckSwitchState();
    }

    public override void OnExitState()
    {
        Debug.Log("Exit Jump State");
        //_context.Animator.SetBool(Constan.AnimJump, false);
    }

    public override void CheckSwitchState()
    {
        if (!PlayerController.Instance.JumpState())
        {
            //_context.Animator.SetTrigger("fall");
            SwitchState(_factory.Fall());
        }
        if (!InputManager.Instance.isJumping && _elapsedTime >= jumpTime && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Idle());
        }
    }
    
    public void JumpHandler()
    {
        var vector3 = PlayerController.Instance.VerticalVelocity;
        vector3.y = Mathf.Sqrt((PlayerController.Instance.JumpHeight * 10) * -2f * PlayerController.Instance.Gravity);
        PlayerController.Instance.VerticalVelocity = vector3;
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
