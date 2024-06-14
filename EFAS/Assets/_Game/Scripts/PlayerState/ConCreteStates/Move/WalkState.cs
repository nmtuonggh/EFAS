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
        // //to jump
        // if (PlayerController.Instance.IsGround() && InputManager.Instance.jumpBtn)
        // {
        //     SwitchState(_factory.Jump());
        // }
        //to stop walk
        if(!InputManager.Instance.IsMoving() && _context.Character.IsGrounded())
        {
            SwitchState(_factory.StopWalk());
        }
        // //to slide
        // if (PlayerController.Instance.IsSliding)
        // {
        //     SwitchState(_factory.Slide());
        // }
        // //to fall
        // if (!PlayerController.Instance.IsGround() && PlayerController.Instance.FallState())
        // {
        //     SwitchState(_factory.Fall());
        // }
        // //to run
        // if(InputManager.Instance.IsMoving()&&InputManager.Instance.runBtnDown && PlayerController.Instance.IsGround())
        // {
        //     SwitchState(_factory.Run());
        // }
    }

    private void MoveHandler()
    {
        Vector2 moveInput = InputManager.Instance.Move;

        // Tính toán hướng xoay dựa trên input của joystick
        float targetRotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + CameraController.Instance.MainCamera.transform.eulerAngles.y;
        Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

        // Xoay đối tượng nhân vật
        CameraController.Instance.PlayerRotationObj.transform.rotation = Quaternion.Slerp(
            CameraController.Instance.PlayerRotationObj.transform.rotation,
            targetRotationQuaternion,
            Time.deltaTime * 10
        );
    
        // Tính toán hướng di chuyển
        //Vector3 targetDir = targetRotationQuaternion * Vector3.forward;
        Vector3 cameraForward = CameraController.Instance.MainCamera.transform.forward;
        Vector3 cameraRight = CameraController.Instance.MainCamera.transform.right;

        // Loại bỏ thành phần y để tránh di chuyển lên xuống
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Chuẩn hóa vector để đảm bảo nó không ảnh hưởng đến tốc độ di chuyển
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Tính toán hướng di chuyển mới dựa trên hướng của camera và input từ joystick
        Vector3 targetDir = cameraForward * moveInput.y + cameraRight * moveInput.x;

        _context.Character.SetMovementDirection(targetDir);
    }
}

