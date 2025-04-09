// using UnityEngine;

// public class VehicleController : MonoBehaviour
// {
//     public WheelCollider frontLeftWheel;
//     public WheelCollider frontRightWheel;
//     public WheelCollider rearLeftWheel;
//     public WheelCollider rearRightWheel;

//     public float maxMotorForce = 1000f; // Lực đẩy tối đa
//     public float maxSteeringAngle = 30f; // Góc rẽ tối đa
//     public float brakeForce = 1000f; // Lực phanh (nếu cần)

//     private Rigidbody rb;

//     void Start()
//     {
//         Debug.Log("VehicleController Start");
//         rb = GetComponent<Rigidbody>();
//         if (rb == null) Debug.LogError("Xe thiếu Rigidbody!");
//     }

//     public void Move(float moveInput, float turnInput)
//     {
//         Debug.Log("Vehicle Move, moveInput = " + moveInput + ", turnInput = " + turnInput);

//         // Tính lực đẩy và rẽ
//         float motor = maxMotorForce * moveInput; // W/S để tiến/lùi
//         float steering = maxSteeringAngle * turnInput; // A/D để rẽ

//         // Áp dụng lực cho bánh trước (rẽ)
//         frontLeftWheel.steerAngle = steering;
//         frontRightWheel.steerAngle = steering;

//         // Áp dụng lực đẩy cho tất cả bánh (4WD)
//         frontLeftWheel.motorTorque = motor;
//         frontRightWheel.motorTorque = motor;
//         rearLeftWheel.motorTorque = motor;
//         rearRightWheel.motorTorque = motor;

//         // Cập nhật vị trí bánh (cho mô hình bánh quay theo)
//         UpdateWheelPoses();
//     }

//     private void UpdateWheelPoses()
//     {
//         UpdateWheelPose(frontLeftWheel);
//         UpdateWheelPose(frontRightWheel);
//         UpdateWheelPose(rearLeftWheel);
//         UpdateWheelPose(rearRightWheel);
//     }

//     private void UpdateWheelPose(WheelCollider collider)
//     {
//         if (collider.transform.childCount == 0) return; // Nếu không có mô hình bánh thì bỏ qua
//         Transform visualWheel = collider.transform.GetChild(0); // Mô hình bánh là child của WheelCollider
//         Vector3 position;
//         Quaternion rotation;
//         collider.GetWorldPose(out position, out rotation);
//         visualWheel.transform.position = position;
//         visualWheel.transform.rotation = rotation;
//     }
// }