using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(StatesMachineController currentContext, FactoryStates playerFactoryState) : base(currentContext,
        playerFactoryState)
    {
    }

    public override void OnEnterState()
    {
        Debug.Log("Enter Idle State");
        _elapsedTime = 0;
        //_context.Animator.SetTrigger(Constan.AnimIdle);
    }

    public override void OnUpdateState()
    {
        MoveHandler();

        //CheckSwitchState();
    }

    protected override void OnExitState()
    {
        _context.Animator.ResetTrigger(Constan.AnimIdle);
    }

    public override void CheckSwitchState()
    {
        //to slide
        if (PlayerController.Instance.IsSliding)
        {
            SwitchState(_factory.Slide());
        }

        //to start walk
        if (InputManager.Instance.IsMoving() && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.StartWalk());
        }

        //to jump
        if (InputManager.Instance.jumpBtn && PlayerController.Instance.IsGround())
        {
            SwitchState(_factory.Jump());
        }
    }
    private void MoveHandler()
    {
        if(InputManager.Instance.IsMoving())
        {
            Debug.Log("call");
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
       
            PlayerController.Instance.SetMovementDirection(targetDir);
        }
    }
}