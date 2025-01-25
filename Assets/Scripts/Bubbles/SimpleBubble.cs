using UnityEngine;

public class SimpleBubble : AbstractBubble
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        var player = other.gameObject.GetComponentInParent<TopPlayerScript>();
        player.Trap(this);
    }
}
