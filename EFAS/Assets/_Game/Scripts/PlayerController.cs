using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float turnSmoothVelocity;
    private CharacterController characterController;
    //move
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] private float speed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float rotateSpeed;
    private Vector3 moveDir;

    //jump
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    private Vector3 verticalVelo;
    
    //camera
    [SerializeField] private float senvity = 2f;
    [SerializeField] private GameObject playerRotationObj;
    public float topClamp = 70.0f;
    public float bottomClamp = -30.0f;
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private GameObject mainCamera;
    
    //Slide
    private bool isSliding;
    private Vector3 slopSlideVelocity;
    //
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        cinemachineTargetYaw = cameraFollow.transform.rotation.eulerAngles.y;
        normalSpeed = speed;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        VerticalControll();
        CheckSlopeSlideVelocity();
        if (slopSlideVelocity == Vector3.zero)
        {
            isSliding = false;
        }
    }

    private float targetRotation;
    private void Move()
    {
        Vector3 inputMoveDir = new Vector3(fixedJoystick.Horizontal, 0.0f , fixedJoystick.Vertical).normalized;
        if(inputMoveDir.magnitude >= 0.1f)
        {
            anim.SetTrigger("walk");
            targetRotation = Mathf.Atan2(inputMoveDir.x, inputMoveDir.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, 0.1f * Time.deltaTime);
            playerRotationObj.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 targetDir = Quaternion.Euler(0f,targetRotation , 0f) * Vector3.forward;
        
            //move player
            characterController.Move(targetDir.normalized * (speed * Time.deltaTime) + new Vector3(0.0f,verticalVelo.y,0.0f ) * Time.deltaTime);
        }
    }
    
    private void VerticalControll()
    {
        if (characterController.isGrounded && verticalVelo.y < 0)
        {
            if (slopSlideVelocity != Vector3.zero)
            {
                isSliding = true;
            }
            
            if(isSliding == false)
            {    
                verticalVelo.y = -1;
            }
        }

        if (isSliding)
        {
            var velocity =slopSlideVelocity;
            velocity.y = verticalVelo.y;
            characterController.Move(velocity * Time.deltaTime);
        }
        
        verticalVelo.y += (gravity*9.8f) * Time.deltaTime;
        characterController.Move(verticalVelo * Time.deltaTime);
    }
    
    public void CameraRotation(Vector2 outputPosition)
    {
        float deltaTimeMultiplier = Time.deltaTime * senvity;

        cinemachineTargetYaw += outputPosition.x * deltaTimeMultiplier;
        cinemachineTargetPitch += outputPosition.y * deltaTimeMultiplier;
        
        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);
        
        cameraFollow.transform.rotation = Quaternion.Euler(cinemachineTargetPitch,
            -cinemachineTargetYaw, 0.0f);
    }
    
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void CheckSlopeSlideVelocity()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 5))
        {
            float angle = Vector3.Angle(hitInfo.normal, Vector3.up);
            if (angle > characterController.slopeLimit)
            {
                slopSlideVelocity = Vector3.ProjectOnPlane(verticalVelo, hitInfo.normal);
                return;
            }
        }

        if (isSliding)
        {
            slopSlideVelocity -=slopSlideVelocity * Time.deltaTime * 5;

            if (slopSlideVelocity.magnitude > 50)
            {
                return;
            }
        }
        slopSlideVelocity = Vector3.zero;
    }

    public void Jump()
    {
        if (characterController.isGrounded && isSliding == false)
        {
            verticalVelo.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
        }
    }

    public void Run()
    {
        speed += 10;
    }

    public void Walke()
    {
        speed = normalSpeed;
    }
}
