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
        MoveHandler();
        CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimWalk);
    }

    public override void CheckSwitchState()
    {
        //to jump
        if (PlayerController.Instance.IsGround() && InputManager.Instance.jumpBtn)
        {
            SwitchState(_factory.Jump());
        }
        //to stop walk
        if(!InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.StopWalk());
        }
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }
        //to fall
        if (!PlayerController.Instance.IsGround() && PlayerController.Instance.FallState())
        {
            SwitchState(_factory.Fall());
        }
        //to run
        if(InputManager.Instance.IsMoving()&&InputManager.Instance.runBtnDown && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Run());
        }
    }

    private void MoveHandler()
    {
        Vector2 moveInput = InputManager.Instance.Move;
    
        // Tính độ lớn của vector di chuyển
        float inputMagnitude = moveInput.magnitude;
    
        // Đảm bảo độ lớn không vượt quá 1
        inputMagnitude = Mathf.Clamp01(inputMagnitude);
    
        // Tính toán hướng xoay dựa trên input của joystick
        float targetRotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + PlayerController.Instance.MainCamera.transform.eulerAngles.y;
        Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

        // Xoay đối tượng nhân vật
        PlayerController.Instance.PlayerRotationObj.transform.rotation = Quaternion.Slerp(
            PlayerController.Instance.PlayerRotationObj.transform.rotation,
            targetRotationQuaternion,
            Time.deltaTime * PlayerController.Instance.SmoothRotation
        );
    
        // Tính toán hướng di chuyển
        Vector3 targetDir = targetRotationQuaternion * Vector3.forward;
    
        // Tính toán tốc độ di chuyển dựa trên độ lớn của vector input
        float speed = PlayerController.Instance.Speed * inputMagnitude;
    
        // Di chuyển nhân vật
        PlayerController.Instance.CharacterController.Move(
            targetDir * (speed * Time.deltaTime) +
            new Vector3(0.0f, PlayerController.Instance.VerticalVelocity.y, 0.0f) * Time.deltaTime
        );
        PlayerController.Instance.SetMovementDirection(targetDir);
    }
}

