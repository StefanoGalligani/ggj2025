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


    private float _lastJumpAt;
    private float _lastDashAt;


    public PlayerState state { get; private set; } = PlayerState.FREE;
    public uint struggles { get; private set; }


    void Start()
    {
        _lastJumpAt = -_jumpCooldownSec;
        _lastDashAt = -_dashCooldownSec;
    }

    void Update()
    {
        ProcessInput();
    }

    async void ProcessInput()
    {
        await Movement(
            jump: Input.GetKeyDown(KeyCode.Space),
            dash: Input.GetKeyDown(KeyCode.D),
            movement: Input.GetAxis("Horizontal")
        );
    }

    async Task Movement(bool jump, bool dash, float movement)
    {
        switch (state)
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
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        transform.parent = bubble.transform;
        transform.localPosition = Vector2.zero;

        state = PlayerState.TRAPPED;
    }

    public void Struggle()
    {
        struggles++;
        if (struggles < 4) return;

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;

        state = PlayerState.FREE;
        struggles = 0;
    }
}

public enum PlayerState { FREE, TRAPPED }
