
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private CharacterController _characterController;

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
    [SerializeField] private bool _isSliding;
    private Vector3 _slopSlideVelocity;
    
    public Vector3 VerticalVelocity
    {
        get => verticalVelocity;
        set => verticalVelocity = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public GameObject PlayerRotationObj => _playerRotationObj;
    public GameObject MainCamera => _mainCamera;
    public CharacterController CharacterController => _characterController;
    public float SmoothRotation => _smoothRotation;
    public float JumpHeight => _jumpHeight;
    public float Gravity => _gravity;
    public bool IsSliding => _isSliding;
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
        _normalSpeed = _speed;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckSlopeSlideVelocity();
        VerticalControl();
        if (_slopSlideVelocity == Vector3.zero)
        {
            _isSliding = false;
        }
    }

    private void VerticalControl()
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
                return;
            }
        }

        _slopSlideVelocity = Vector3.zero;
    }

    public bool IsGround()
    {
        return _characterController.isGrounded;
    }

    public bool JumpState()
    {
        if (verticalVelocity.y > 1f)
        {
            return true;
        }
        return false;
    }
    
    public bool FallState()
    {
        if (verticalVelocity.y < -8f)
        {
            return true;
        }
        return false;
    }
}