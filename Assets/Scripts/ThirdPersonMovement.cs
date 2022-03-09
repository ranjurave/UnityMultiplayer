using UnityEngine;
using Photon.Pun;

public class ThirdPersonMovement : MonoBehaviour {
    private CharacterController controller;
    private Transform CameraTransform;
    private float speed = 6f;
    private float turnSmoothVelocity;

    public Transform bulletSpawnPoint;
    public GameObject bullet;

    public PhotonView photonView;

    void Start() {
        controller = GetComponent<CharacterController>();
        Camera PlayerCamera = GetComponentInChildren<Camera>();
        CameraTransform = PlayerCamera.transform;
    }
    void Update() {
        if(PhotonNetwork.InRoom && !photonView.IsMine) {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude > 0) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
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


