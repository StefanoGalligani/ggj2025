using UnityEngine;

public class SimpleBubble : AbstractBubble
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("IsPlayer");

        var player = other.gameObject.GetComponentInParent<TopPlayerScript>();
        player.Trap(this);
    }
}
