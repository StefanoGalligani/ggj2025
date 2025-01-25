using UnityEngine;

public class SnailScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _velocity;
    [SerializeField] private RectTransform _movementArea;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private GameObject _cannonArea;
    private Vector2 _targetPosition;
    private AbstractBubble _currentBubble;
    private PowerupType _powerupType = PowerupType.None;

    void Start()
    {
        _targetPosition = transform.position;
    }

    private bool Inside(Vector2 pos, RectTransform area)
    {
        if (pos.x < area.position.x) return false;
        if (pos.x > area.position.x + area.localScale.x) return false;
        if (pos.y < area.position.y) return false;
        if (pos.y > area.position.y + area.localScale.y + 0.5f) return false;
        return true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            Vector2 newTargetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Inside(newTargetPosition, _movementArea))
            {
                _targetPosition = newTargetPosition;
                if (_targetPosition.y > _movementArea.position.y + _movementArea.localScale.y - 0.5f) {
                    _targetPosition.y = _movementArea.position.y + _movementArea.localScale.y - 0.5f;
                }
            }
        }
        float dist = Vector3.Distance(transform.position, (Vector3)_targetPosition);
        if (dist > 0.1f)
        {
            transform.position += ((Vector3)_targetPosition - transform.position) / dist * Time.deltaTime * _velocity;
            _line.SetPosition(1, transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Powerup":
                _currentBubble = other.gameObject.GetComponent<PowerupScript>().powerBubble;
                _powerupType = other.gameObject.GetComponent<PowerupScript>().powerupType;
                Destroy(other.gameObject);
                _cannonArea.SetActive(true);

                break;

            case "Cannon":
                if (_powerupType == PowerupType.None) break;

                other.gameObject.GetComponent<CannonScript>().SetPowerup(_powerupType, _currentBubble);
                _powerupType = PowerupType.None;
                _cannonArea.SetActive(false);

                break;
        }
    }
}
