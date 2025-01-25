using UnityEngine;

public abstract class AbstractBubble : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _velocity;

    public void Shoot(Vector2 direction)
    {
        _rb.AddForce(_velocity * direction, ForceMode2D.Impulse);
    }
}
