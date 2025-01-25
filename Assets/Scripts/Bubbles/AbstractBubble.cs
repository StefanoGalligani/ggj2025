using UnityEngine;

public abstract class AbstractBubble : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _velocity;
    [SerializeField] private Vector2 _targetYVelocityRange;

    public void Shoot(Vector2 direction)
    {
        _rb.AddForce(_velocity * direction, ForceMode2D.Impulse);
    }

    protected void FixedUpdate()
    {
        _rb.linearVelocityX *= 1 - Time.fixedDeltaTime;

        float targetYVelocity = Mathf.Clamp(_rb.linearVelocityY, _targetYVelocityRange.x, _targetYVelocityRange.y);
        float deltaYVelocity = targetYVelocity - _rb.linearVelocityY;
        _rb.linearVelocityY += deltaYVelocity * Time.fixedDeltaTime;
    }
}
