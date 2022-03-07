using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float speed = 20f;
    Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Ground")) {
        Destroy(gameObject);
        }
    }
}