using System;
using System.Threading.Tasks;

using UnityEngine;

public class TopPlayerScript : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rigidbody;


    [Range(0.1f, 10.0f)]
    [SerializeField] float jumpForce = 1;

    [Range(0.1f, 10.0f)]
    [SerializeField] float jumpCooldownSec = 1;


    [Range(0.1f, 10.0f)]
    [SerializeField] float movementSpeed = 1;


    [Range(0.1f, 10.0f)]
    [SerializeField] float dashForce = 1;

    [Range(0.1f, 10.0f)]
    [SerializeField] float dashCooldownSec = 3;

    [Range(50, 2000)]
    [SerializeField] int dashDurationMs = 100;


    private float lastJumpAt;
    private float lastDashAt;

    void Start()
    {
        lastJumpAt = -jumpCooldownSec;
        lastDashAt = -dashCooldownSec;
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
        if (jump && Time.time >= lastJumpAt + jumpCooldownSec)
        {
            Jump();
            lastJumpAt = Time.time;
        }

        if (dash && Time.time >= lastDashAt + dashCooldownSec)
        {
            await Dash(movement);
            lastDashAt = Time.time;
        }

        Move(movement);
    }

    void Jump()
    {
        rigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    async Task Dash(float direction)
    {
        var dashVec = new Vector2(direction, 0).normalized * dashForce;
        rigidbody.linearVelocity = dashVec;

        await Task.Delay(dashDurationMs);

        rigidbody.linearVelocity = Vector2.zero;
    }

    void Move(float movement)
    {
        rigidbody.AddForceX(movement * movementSpeed);
    }
}
