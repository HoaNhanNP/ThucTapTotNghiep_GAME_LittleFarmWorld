using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Nhân vật
    public float distance = 5f; // Khoảng cách từ nhân vật
    public float height = 1.5f; // Chiều cao so với nhân vật
    public float smoothSpeed = 10f; // Tốc độ đuổi theo

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position + Vector3.up * 1f); // Nhìn vào đầu nhân vật
    }
}