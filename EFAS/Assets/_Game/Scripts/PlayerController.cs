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
    private CharacterController _characterController;
    private Animator anim;

    [Header("Player Move")] [SerializeField]
    private float speed;

    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _smoothRotation;

    //jump
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight;
    private Vector3 verticalVelocity;

    //camera
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _playerRotationObj;

    //Slide
    private bool _isSliding;
    private Vector3 _slopSlideVelocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _normalSpeed = speed;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        VerticalControll();
        CheckSlopeSlideVelocity();
        if (_slopSlideVelocity == Vector3.zero)
        {
            _isSliding = false;
        }
    }

    private void Move()
    {
        if (InputManager.Instance.Move.magnitude >= 0.1f)
        {
            float targetRotation =
                Mathf.Atan2(InputManager.Instance.Move.x, InputManager.Instance.Move.y) * Mathf.Rad2Deg +
                _mainCamera.transform.eulerAngles.y;
            Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

            _playerRotationObj.transform.rotation = Quaternion.Slerp(_playerRotationObj.transform.rotation,
                targetRotationQuaternion, Time.deltaTime * _smoothRotation);
            Vector3 targetDir = targetRotationQuaternion * Vector3.forward;

            _characterController.Move(targetDir.normalized * (speed * Time.deltaTime) +
                                     new Vector3(0.0f, verticalVelocity.y, 0.0f) * Time.deltaTime);
        }
    }


    private void VerticalControll()
    {
        if (_characterController.isGrounded && verticalVelocity.y < 0)
        {
            if (_slopSlideVelocity != Vector3.zero)
            {
                _isSliding = true;
            }

            if (!_isSliding)
            {
                verticalVelocity.y = -1;
            }
        }

        if (_isSliding)
        {
            Vector3 velocity = _slopSlideVelocity;
            velocity.y = verticalVelocity.y;
            _characterController.Move(velocity * Time.deltaTime);
        }

        verticalVelocity.y += (_gravity * 9.8f) * Time.deltaTime;
        _characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void CheckSlopeSlideVelocity()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 5))
        {
            float angle = Vector3.Angle(hitInfo.normal, Vector3.up);
            if (angle > _characterController.slopeLimit)
            {
                _slopSlideVelocity = Vector3.ProjectOnPlane(verticalVelocity, hitInfo.normal);
                return;
            }
        }

        if (_isSliding)
        {
            _slopSlideVelocity -= _slopSlideVelocity * Time.deltaTime * 5;

            if (_slopSlideVelocity.magnitude > 70)
            {
                return;
            }
        }

        _slopSlideVelocity = Vector3.zero;
    }

    public void Jump()
    {
        if (_characterController.isGrounded && _isSliding == false)
        {
            verticalVelocity.y = Mathf.Sqrt((_jumpHeight * 10) * -2f * _gravity);
        }
    }

    public void Run()
    {
        speed += 10;
    }

    public void Walke()
    {
        speed = _normalSpeed;
    }
}