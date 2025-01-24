using UnityEngine;
using UnityEngine.InputSystem;

public class SnailScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _velocity;
    private Vector2 _targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _targetPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
            Debug.Log("MousePosition: " + _camera.ScreenToViewportPoint(Input.mousePosition));
            _targetPosition = (_camera.ScreenToViewportPoint(Input.mousePosition) * 2 - new Vector3(0.5f,0.5f))
                * _camera.orthographicSize + _camera.transform.position;
        }
        float dist = Vector3.Distance(transform.position, (Vector3)_targetPosition);
        if (dist > 0.1f) {
            transform.position += ((Vector3)_targetPosition - transform.position)/dist * Time.deltaTime * _velocity;
        }
    }
}
