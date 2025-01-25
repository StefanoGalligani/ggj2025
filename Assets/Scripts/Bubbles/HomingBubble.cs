using UnityEngine;

public class HomingBubble : SimpleBubble
{
    [Range(0.5f, 3.0f)]
    [SerializeField] private float _homingForce = 1;


    private TopPlayerScript _player;


    void Start()
    {
        _player = FindAnyObjectByType<TopPlayerScript>();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        _rb.linearVelocityX += _player.transform.position.x * Time.fixedDeltaTime * _homingForce;
    }
}
