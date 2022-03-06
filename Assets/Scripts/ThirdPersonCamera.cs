using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;
    public Transform playerPos;
    private Transform camTransform;
    private Camera cam;
    private float distance = 10;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 5.0f;
    private Vector3 lookatPos;
    private void Start() {
        camTransform = transform;
        cam = Camera.main;
    }
    private void Update() {
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += -Input.GetAxis("Mouse Y") * sensitivityY;
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }
    private void LateUpdate() {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = playerPos.position + rotation * dir;
        lookatPos = playerPos.position;
        lookatPos.y += 4.0f;
        camTransform.LookAt(lookatPos);
    }
}

