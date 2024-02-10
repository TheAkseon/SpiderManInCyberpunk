using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    [SerializeField] private ParticleSystem _warpSpeedEffect;

    // New movement
    [SerializeField] private float speed = 2;
    [SerializeField] private float speedX = 2;
    [SerializeField] float _maxPosX = 3f;
    [SerializeField] private float _timeApplyInvulnerble = 0.1f;

    [Space]
    [Range(0, 1)][SerializeField] float _smoothHorizontalTime = 0.2f;

    private Vector3 _playerPosition;

    private float _xVelocity;
    private float _xPos;
    private float _zPos;

    private float _originalSpeed;
    private bool _canMove = false;
    private bool _extendNitro = false;
    private bool _isInvulnerble = false;

    public bool IsInvulnerble => _isInvulnerble;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _originalSpeed = speed;
        _zPos = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            HandleInput();
            Move();
        }
    }

    private void OnEnable()
    {
        PlayerInput.OnPointerDrag += HorizontalMovement;
    }
    private void OnDisable()
    {
        PlayerInput.OnPointerDrag -= HorizontalMovement;
    }

    public void ApplyInvulnerable()
    {
        _isInvulnerble = true;
        Invoke(nameof(StopInvulnerable), _timeApplyInvulnerble);
    }

    private void StopInvulnerable()
    {
        _isInvulnerble = false;
    }

    public void HorizontalMovement(float xMovement)
    {
        _playerPosition.x = xMovement * _maxPosX;
    }

    private void Move()
    {
        Vector3 newPosition = transform.position + Vector3.forward * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void HandleInput()
    {
        if (InputManager.IsMoving("Mouse X") && InputManager.IsLeftMouseButtonDown())
        {
            float movementVectorX = InputManager.GetAxis("Mouse X");
            float newPositionX = Mathf.Clamp(transform.position.x + movementVectorX, -_maxPosX, _maxPosX);
            
            Vector3 newPosition = transform.position + new Vector3(newPositionX - transform.position.x, 0, 0) * speedX * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    public void StopMovement() => _canMove = false;

    public void ResumeMovement() => _canMove = true;

    public bool CanMove() => _canMove;

    public void ApplyNitro(float timeApplyNitro, float nitroMultiplier)
    {
        if (speed == _originalSpeed)
        {
            speed *= nitroMultiplier;
            _warpSpeedEffect.gameObject.SetActive(true);
        }
        else
        {
            _extendNitro = true;
        }

        Invoke(nameof(StopNitro), timeApplyNitro);
    }

    private void StopNitro()
    {
        if (!_extendNitro)
        {
            speed = _originalSpeed;
            _warpSpeedEffect.gameObject.SetActive(false);
        }
        _extendNitro = false;
    }
}
    