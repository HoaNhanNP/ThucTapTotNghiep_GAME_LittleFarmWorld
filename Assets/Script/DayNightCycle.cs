using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun; // Gắn Directional Light (mặt trời) vào đây trong Inspector
    public float dayDuration = 120f; // Thời gian một ngày trong game (tính bằng giây, ví dụ 2 phút)
    private float timeOfDay = 0f; // Từ 0 (nửa đêm) đến 1 (nửa đêm ngày hôm sau)

    void Update()
    {
        // Tăng thời gian dựa trên thời gian thực
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay >= 1f) timeOfDay -= 1f; // Quay lại 0 khi hết ngày

        // Xoay mặt trời (0° = bình minh, 180° = nửa đêm)
        float sunAngle = timeOfDay * 360f;
        sun.transform.rotation = Quaternion.Euler(sunAngle - 90f, 0, 0);

        // Điều chỉnh độ sáng của mặt trời
        if (sunAngle < 180f)
            sun.intensity = Mathf.Clamp01(sunAngle / 90f); // Sáng dần từ 0 đến 1
        else
            sun.intensity = Mathf.Clamp01((360f - sunAngle) / 90f); // Tối dần từ 1 về 0
    }
}