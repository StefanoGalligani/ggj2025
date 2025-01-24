using UnityEngine;

public class TopPlayerScript : MonoBehaviour {
    [SerializeField] new Rigidbody2D rigidbody;

    [Range(0.1f, 10.0f)]
    [SerializeField] float jumpForce = 1;

    void Start() {}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
}
