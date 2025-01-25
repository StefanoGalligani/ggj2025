using System.Threading.Tasks;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _snailArea;
    [SerializeField] private Transform _shootStartPosition;
    [SerializeField] private AbstractBubble _bubblePrefab;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float _cooldownSec = 1;
    [SerializeField] private float _lockTime = 3;
    private float _lastShotAt;
    private AbstractBubble _specialBubble;
    private bool _raffic = false;
    private float _currentLockTime = 0;

    void Start()
    {
        _lastShotAt = -_cooldownSec;
    }

    async Task Update()
    {
        if (_currentLockTime > 0) {
            _currentLockTime -= Time.deltaTime;
            return;
        }
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.y >= _snailArea.position.y + _snailArea.localScale.y)
        {
            transform.up = (Vector3)mousePos - transform.position;
            transform.GetChild(0).rotation = Quaternion.identity;
            if (Time.timeScale < 0.1f) return;

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

                if (_raffic) {
                    _raffic = false;
                    await ShootRaffic(bubble);
                } else {
                    bubble.Shoot(transform.up);
                }
                _lastShotAt = Time.time;
            }
        }
    }

    private async Task ShootRaffic(AbstractBubble bubble) {
        bubble.Shoot(transform.up);
        await Task.Delay(250);
        bubble = GameObject.Instantiate<AbstractBubble>(bubble, _shootStartPosition.position, Quaternion.identity);
        bubble.Shoot(transform.up);
        await Task.Delay(250);
        bubble = GameObject.Instantiate<AbstractBubble>(bubble, _shootStartPosition.position, Quaternion.identity);
        bubble.Shoot(transform.up);
    }

    public void SetPowerup(PowerupType type, AbstractBubble bubble)
    {
        switch (type) {
            case PowerupType.SpecialBubble:
                _specialBubble = bubble;
                break;
            case PowerupType.BubbleRaffic:
                _raffic = true;
                break;
            case PowerupType.CannonLock:
                _currentLockTime = _lockTime;
                break;
        }
    }
}
