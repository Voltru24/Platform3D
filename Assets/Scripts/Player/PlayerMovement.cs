using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _decelerationWhenFalling;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _boxCastMaxDistance;
    [SerializeField] private float _boxCastHalfExtents;

    private Player _player;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isStun;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.ChangeStun += ChangeStun;
    }

    private void OnDisable()
    {
        _player.ChangeStun -= ChangeStun;
    }

    private void Update()
    {
        if (_player.IsAlive && _isStun == false)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (_player.IsAlive && _isStun == false)
        {
            Move();
        }
    }

    private void Move()
    {
        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            Vector3 direction = (Vector3.right * _horizontalInput) + (Vector3.forward * _verticalInput);

            if (CheckIsOnGround())
            {
                _rigidbody.AddForce(_moveSpeed * direction);
            }
            else
            {
                _rigidbody.AddForce((_moveSpeed/ _decelerationWhenFalling) * direction);
            }
        }
    }

    private void Jump()
    {
        if (CheckIsOnGround())
        {
            _rigidbody.AddForce(Vector2.up  * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool CheckIsOnGround()
    {
        RaycastHit[] hits = Physics.BoxCastAll(_transform.position, _transform.localScale * _boxCastHalfExtents, Vector3.down, new Quaternion(), _boxCastMaxDistance, LayerMask.GetMask("Ground"));

        if (hits.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangeStun(bool isStun)
    {
        _isStun = isStun;
    }  
}

