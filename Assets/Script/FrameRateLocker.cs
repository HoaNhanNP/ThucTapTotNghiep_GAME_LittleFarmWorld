using UnityEngine;

public class FrameRateLocker : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60; // Có thể chỉnh trong Inspector

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}