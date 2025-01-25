using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [SerializeField] private AbstractBubble _powerBubble;
    public AbstractBubble GetBubble() {
        return _powerBubble;
    }
}
