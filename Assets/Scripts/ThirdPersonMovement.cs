using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour {
    private CharacterController controller;
    public Transform cam;
    private float speed = 6f;
    private float turnSmoothVelocity;

    public Transform bulletSpawnPoint;
    public GameObject bullet;

    void Start() {
        controller = GetComponent<CharacterController>();
    }
    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude > 0) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            Vector3 moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            FireProjectile();
        }
    }

    void FireProjectile() {
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}


