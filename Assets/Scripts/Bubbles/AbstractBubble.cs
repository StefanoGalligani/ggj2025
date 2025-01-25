using UnityEngine;

public abstract class AbstractBubble : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _velocity;
    [SerializeField] private Vector2 _targetYVelocityRange;
    public void Shoot(Vector2 direction) {
        _rb.AddForce(_velocity * direction, ForceMode2D.Impulse);
    }

    public void FixedUpdate() {
        _rb.linearVelocityX = _rb.linearVelocityX * (1 - Time.fixedDeltaTime);

        float targetYVelocity = Mathf.Clamp(_rb.linearVelocityY, _targetYVelocityRange.x, _targetYVelocityRange.y);
        float deltaYVelocity = targetYVelocity - _rb.linearVelocityY;
        _rb.linearVelocityY += deltaYVelocity * Time.fixedDeltaTime; 
    }
}
