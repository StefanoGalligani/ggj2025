using UnityEngine;

public class SimpleBubble : AbstractBubble
{
    void Start() { }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        var player = other.gameObject.GetComponent<TopPlayerScript>();
        player.Trap(this);
    }

    void Update() { }
}
