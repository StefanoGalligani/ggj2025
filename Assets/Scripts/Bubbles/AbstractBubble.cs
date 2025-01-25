using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractBubble : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected float _velocity;
    [SerializeField] private Vector2 _targetYVelocityRange;

    private void Start() {
        transform.GetChild(0).GetComponent<Animator>().speed = 0;
    }

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

    public virtual void EnteredBubble() {}
    public async Task ExitedBubble() {
        Destroy(GetComponent<Collider2D>());
        transform.GetChild(0).GetComponent<Animator>().speed = 1;
        await Task.Delay(200);
        Destroy(gameObject);
    }
}
