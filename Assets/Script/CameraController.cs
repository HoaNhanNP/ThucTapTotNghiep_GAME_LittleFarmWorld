using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Gán thủ công trong Inspector
    private float xRotation = 0f;
    public bool isLocked = false; // Trạng thái khóa camera

    void Start()
    {
        if (playerBody == null)
        {
            Debug.LogError("PlayerBody is not assigned in the Inspector!");
            return;
        }
        // Khởi tạo khóa chuột sẽ được MouseCursorController xử lý
    }

    void Update()
    {
        // Chỉ xử lý góc nhìn khi không khóa và MouseLook được bật
        if (!isLocked && MouseCursorController.Instance != null && MouseCursorController.Instance.IsMouseLookEnabled())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Xoay dọc (Pitch) cho camera
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Xoay ngang (Yaw) cho Player
            if (playerBody != null)
            {
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }

    public void LockCamera(bool lockState)
    {
        isLocked = lockState;
    }
}