using UnityEngine;

public abstract class AbstractBubble : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _velocity;
    public void Shoot(Vector2 direction) {
        _rb.AddForce(_velocity * direction, ForceMode2D.Impulse);
    }
}
