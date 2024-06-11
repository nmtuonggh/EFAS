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
    public static PlayerController Instance;
    private CharacterController _characterController;
    private Animator anim;

    [Header("Player Move")] [SerializeField]
    private float _speed;
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
    //bool check event
    private bool _isJumping = false;
    private bool _isRunning = false;

    public GameObject PlayerRotationObj
    {
        get => _playerRotationObj;
        set => _playerRotationObj = value;
    }
    public bool IsJumping
    {
        get => _isJumping;
        set => _isJumping = value;
    }

    public bool IsRunning
    {
        get => _isRunning;
        set => _isRunning = value;
    }

    public Vector3 VerticalVelocity
    { 
        get => verticalVelocity;
        set => verticalVelocity = value;
    }
    
    public GameObject MainCamera
    {
        get => _mainCamera;
        set
        {
            _mainCamera = value;
        }
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public CharacterController CharacterController
    {
        get => _characterController;
        set => _characterController = value;
    }

    public float SmoothRotation
    {
        get => _smoothRotation;
        set => _smoothRotation = value;
    }

    public float JumpHeight => _jumpHeight;

    public float Gravity => _gravity;

    public bool IsSliding => _isSliding;

    public Vector3 SlopSlideVelocity
    {
        get => _slopSlideVelocity;
        set => _slopSlideVelocity = value;
    }

    public float NormalSpeed => _normalSpeed;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        _normalSpeed = _speed;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        OnAir();
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

            _characterController.Move(targetDir.normalized * (_speed * Time.deltaTime) +
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
        HandlerSlide();
        _slopSlideVelocity = Vector3.zero;
    }

    private void HandlerSlide()
    {
        if (_isSliding)
        {
            _slopSlideVelocity -= _slopSlideVelocity * Time.deltaTime * 5;

            if (_slopSlideVelocity.magnitude > 70)
            {
                return ;
            }
        }
        _slopSlideVelocity = Vector3.zero;
    }

    public void Jump()
    {
        if (_characterController.isGrounded && _isSliding == false)
        {
            _isJumping = true;
            verticalVelocity.y = Mathf.Sqrt((_jumpHeight * 10) * -2f * _gravity);
        }
    }

    public void Run()
    {
        _isRunning = true;
        _speed += 10;
    }

    public void Walke()
    {
        _speed = _normalSpeed; 
    }

    public bool IsGround()
    {
        return _characterController.isGrounded;
    }

    public bool JumpState()
    {
        if(verticalVelocity.y > 0)
        {
            return true;
        }
        if(verticalVelocity.y <-1f )
        {
            return false;
        }
        return false;
    }

    private void OnAir()
    {
        if (InputManager.Instance.IsMoving())
        {
            float targetRotation =
                Mathf.Atan2(InputManager.Instance.Move.x, InputManager.Instance.Move.y) * Mathf.Rad2Deg +
                _mainCamera.transform.eulerAngles.y;
            Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

            _playerRotationObj.transform.rotation = Quaternion.Slerp(_playerRotationObj.transform.rotation,
                targetRotationQuaternion, Time.deltaTime * _smoothRotation);
            Vector3 targetDir = targetRotationQuaternion * Vector3.forward;

            _characterController.Move(targetDir.normalized * (_speed * Time.deltaTime) +
                                      new Vector3(0.0f, verticalVelocity.y, 0.0f) * Time.deltaTime);
        }
        
    }
}