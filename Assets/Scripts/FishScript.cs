using UnityEngine;

public class FishScript : MonoBehaviour
{
    [SerializeField] private Vector2 _bounds;
    [SerializeField] private float _velocity;
    private bool _facingRight;

    private void Start() {
        _facingRight = Random.Range(0,2) == 0;
    }

    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().flipX = !_facingRight;
        transform.position = new Vector3(transform.position.x + Time.deltaTime * _velocity * (_facingRight ? 1 : -1), transform.position.y, 0);
        if (transform.position.x > _bounds.y) {
            _facingRight = false;
        } else if (transform.position.x < _bounds.x) {
            _facingRight = true;
        }
    }
}
