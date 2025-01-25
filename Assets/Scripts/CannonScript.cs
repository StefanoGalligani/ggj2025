using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _snailArea;
    [SerializeField] private Transform _shootStartPosition;
    [SerializeField] private AbstractBubble _bubblePrefab;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float _cooldownSec = 1;


    private float _lastShotAt;


    private AbstractBubble _specialBubble;

    void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.y >= _snailArea.position.y + _snailArea.localScale.y)
        {
            if (mousePos.y >= _snailArea.position.y + _snailArea.localScale.y + 1)
            {
                transform.up = (Vector3)mousePos - transform.position;
            }

            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            {
                if (Time.time < _lastShotAt + _cooldownSec) return;

                AbstractBubble bubble;
                if (_specialBubble != null)
                {
                    bubble = GameObject.Instantiate<AbstractBubble>(_specialBubble, _shootStartPosition.position, Quaternion.identity);
                    _specialBubble = null;
                }
                else
                {
                    bubble = GameObject.Instantiate<AbstractBubble>(_bubblePrefab, _shootStartPosition.position, Quaternion.identity);
                }


                bubble.Shoot(transform.up);
                _lastShotAt = Time.time;
            }
        }
    }

    public void SetSpecialBubble(AbstractBubble bubble)
    {
        _specialBubble = bubble;
    }
}
