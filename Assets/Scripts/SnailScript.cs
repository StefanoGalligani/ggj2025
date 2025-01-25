using UnityEngine;
using UnityEngine.InputSystem;

public class SnailScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _velocity;
    [SerializeField] private RectTransform _movementArea;
    [SerializeField] private LineRenderer _line;
    private Vector2 _targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _targetPosition = transform.position;
    }

    private bool Inside(Vector2 pos, RectTransform area) {
        if (pos.x < area.position.x) return false;
        if (pos.x > area.position.x + area.localScale.x) return false;
        if (pos.y < area.position.y) return false;
        if (pos.y > area.position.y + area.localScale.y) return false;
        return true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
            Vector2 newTargetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            if (Inside(newTargetPosition, _movementArea)) {
                _targetPosition = newTargetPosition;
            }
        }
        float dist = Vector3.Distance(transform.position, (Vector3)_targetPosition);
        if (dist > 0.1f) {
            transform.position += ((Vector3)_targetPosition - transform.position)/dist * Time.deltaTime * _velocity;
            _line.SetPosition(1, transform.position);
        }
    }
}
