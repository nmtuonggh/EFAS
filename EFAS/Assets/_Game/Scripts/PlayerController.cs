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
    private CharacterController characterController;
    private Animator anim;

    [Header("Player Move")] [SerializeField]
    private float speed;

    [SerializeField] private float normalSpeed;
    [SerializeField] private float smoothRotation;

    //jump
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    private Vector3 verticalVelocity;

    //camera
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerRotationObj;

    //Slide
    private bool isSliding;
    private Vector3 slopSlideVelocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
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

    private void Move()
    {
        if (InputManager.Instance.move.magnitude >= 0.1f)
        {
            float targetRotation =
                Mathf.Atan2(InputManager.Instance.move.x, InputManager.Instance.move.y) * Mathf.Rad2Deg +
                mainCamera.transform.eulerAngles.y;
            Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

            playerRotationObj.transform.rotation = Quaternion.Slerp(playerRotationObj.transform.rotation,
                targetRotationQuaternion, Time.deltaTime * smoothRotation);
            Vector3 targetDir = targetRotationQuaternion * Vector3.forward;

            characterController.Move(targetDir.normalized * (speed * Time.deltaTime) +
                                     new Vector3(0.0f, verticalVelocity.y, 0.0f) * Time.deltaTime);
        }
    }


    private void VerticalControll()
    {
        if (characterController.isGrounded && verticalVelocity.y < 0)
        {
            if (slopSlideVelocity != Vector3.zero)
            {
                isSliding = true;
            }

            if (!isSliding)
            {
                verticalVelocity.y = -1;
            }
        }

        if (isSliding)
        {
            Vector3 velocity = slopSlideVelocity;
            velocity.y = verticalVelocity.y;
            characterController.Move(velocity * Time.deltaTime);
        }

        verticalVelocity.y += (gravity * 9.8f) * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void CheckSlopeSlideVelocity()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 5))
        {
            float angle = Vector3.Angle(hitInfo.normal, Vector3.up);
            if (angle > characterController.slopeLimit)
            {
                slopSlideVelocity = Vector3.ProjectOnPlane(verticalVelocity, hitInfo.normal);
                return;
            }
        }

        if (isSliding)
        {
            slopSlideVelocity -= slopSlideVelocity * Time.deltaTime * 5;

            if (slopSlideVelocity.magnitude > 70)
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
            verticalVelocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
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