using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
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
    //
    private Vector3 playerInput;
    
    private void Start()
    {
        normalSpeed = speed;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Movement();
        Move();
        VerticalControll();
    }

    private void Movement()
    { 
        moveDir.Set(fixedJoystick.Horizontal, 0 , fixedJoystick.Vertical);

        if (moveDir.magnitude != 0)
        {
            Vector3 cameraRelavetiveMovement = ConvertToCameraSpace(moveDir);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cameraRelavetiveMovement, Vector3.up), rotateSpeed * Time.deltaTime );
            characterController.Move(cameraRelavetiveMovement * speed * Time.deltaTime);
        }
    }

    private float targetRotation;
    private void Move()
    {
        Vector3 inputMoveDir = new Vector3(fixedJoystick.Horizontal, 0 , fixedJoystick.Vertical);
        if(inputMoveDir!= Vector3.zero)
        {
            targetRotation = Mathf.Atan2(inputMoveDir.x, inputMoveDir.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetRotation.y, Vector3.up), rotateSpeed * Time.deltaTime );
            
            Vector3 targetDir = Quaternion.Euler(0f,targetRotation , 0f) * Vector3.forward;
        
            //move player
            characterController.Move(targetDir.normalized * (speed * Time.deltaTime) + new Vector3(0.0f,verticalVelo.y,0.0f ) * Time.deltaTime);
        }
    }

    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;
        
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotateToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotateToCameraSpace.y = currentYValue;
        return vectorRotateToCameraSpace;
    }
    
    private void VerticalControll()
    {
        if (characterController.isGrounded && verticalVelo.y < 0)
        {
            verticalVelo.y = -1;
        }

        verticalVelo.y += (gravity*9.8f) * Time.deltaTime;
        characterController.Move(verticalVelo * Time.deltaTime);
    }

    public void Jump()
    {
        verticalVelo.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
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
