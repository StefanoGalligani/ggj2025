using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _snailArea;
    [SerializeField] private Transform _shootStartPosition;
    [SerializeField] private AbstractBubble _bubblePrefab;
     
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.y >= _snailArea.position.y + _snailArea.localScale.y) {
            if (mousePos.y >= _snailArea.position.y + _snailArea.localScale.y + 1) {
                transform.up = (Vector3)mousePos - transform.position;
            }
            
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
                AbstractBubble bubble = GameObject.Instantiate<AbstractBubble>(_bubblePrefab, _shootStartPosition.position, Quaternion.identity);
                bubble.Shoot(transform.up);
            }
        }
    }
}
