using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [SerializeField] public AbstractBubble powerBubble;
    [SerializeField] public PowerupType powerupType;
}

public enum PowerupType {
    None,
    SpecialBubble,
    BubbleRaffic,
    CannonLock
}