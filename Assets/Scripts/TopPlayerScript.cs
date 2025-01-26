using System;
using System.Threading.Tasks;

using UnityEngine;

public class TopPlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float _jumpForce = 1;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float _jumpCooldownSec = 1;


    [Range(0.1f, 10.0f)]
    [SerializeField] private float _movementSpeed = 1;


    [Range(0.1f, 10.0f)]
    [SerializeField] private float _dashForce = 1;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float _dashCooldownSec = 3;

    [Range(50, 2000)]
    [SerializeField] private int _dashDurationMs = 100;

    [Range(2, 20)]
    [SerializeField] private int _terminalVelocity = 6;

    [Range(0.1f, 2)]
    [SerializeField] private float _stunnedTime = 1;
    [SerializeField] private Sprite[] _sprites;

    private float _lastJumpAt;
    private float _lastDashAt;
    private float _currentStunTime;
    private bool _stunned;

    public PlayerState State { get; private set; } = PlayerState.FREE;
    public uint Struggles { get; private set; }

    void Start()
    {
        _lastJumpAt = -_jumpCooldownSec;
        _lastDashAt = -_dashCooldownSec;
    }

    void Update()
    {
        if (_stunned)
        {
            _currentStunTime += Time.deltaTime;
            if (_currentStunTime >= _stunnedTime)
            {
                _stunned = false;
                GetComponentInChildren<SpriteRenderer>().sprite = _sprites[0];
            }
        }
        else
        {
            ProcessInput();
        }
        _rigidbody.linearVelocityY = Mathf.Clamp(_rigidbody.linearVelocityY, -_terminalVelocity, _terminalVelocity);
    }

    async void ProcessInput()
    {
        await Movement(
            jump: Input.GetKeyDown(KeyCode.Space),
            dash: Input.GetKeyDown(KeyCode.LeftControl),
            movement: Input.GetAxis("Horizontal")
        );
    }

    async Task Movement(bool jump, bool dash, float movement)
    {
        switch (State)
        {
            case PlayerState.FREE:
                if (jump && Time.time >= _lastJumpAt + _jumpCooldownSec)
                {
                    Jump();
                    _lastJumpAt = Time.time;
                }

                if (dash && Time.time >= _lastDashAt + _dashCooldownSec)
                {
                    await Dash(movement);
                    _lastDashAt = Time.time;
                }

                Move(movement);
                break;

            case PlayerState.TRAPPED:
                if (jump) Struggle();
                break;
        }
    }

    void Jump()
    {
        _rigidbody.AddForceY(_jumpForce, ForceMode2D.Impulse);
    }

    async Task Dash(float direction)
    {
        var dashVec = new Vector2(direction, 0).normalized * _dashForce;
        _rigidbody.linearVelocity = dashVec;

        await Task.Delay(_dashDurationMs);

        _rigidbody.linearVelocity = Vector2.zero;
    }

    void Move(float movement)
    {
        _rigidbody.AddForceX(movement * _movementSpeed);
    }

    public void Trap(AbstractBubble bubble)
    {
        bubble.EnteredBubble();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        transform.parent = bubble.transform;
        transform.localPosition = Vector2.zero;

        State = PlayerState.TRAPPED;
        GetComponentInChildren<SpriteRenderer>().sprite = _sprites[1];
    }

    public void Struggle()
    {
        Struggles++;
        if (Struggles < 4) return;

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.linearVelocity = Vector2.zero;

        _ = transform.parent.GetComponent<AbstractBubble>().ExitedBubble();
        transform.parent = null;
        transform.localScale = Vector2.one;

        State = PlayerState.FREE;
        Struggles = 0;
        GetComponentInChildren<SpriteRenderer>().sprite = _sprites[0];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            _rigidbody.AddForce((transform.position - other.transform.position).normalized * 5, ForceMode2D.Impulse);
            _currentStunTime = 0;
            _stunned = true;
            GetComponentInChildren<SpriteRenderer>().sprite = _sprites[2];
        }
    }
}

public enum PlayerState { FREE, TRAPPED }
