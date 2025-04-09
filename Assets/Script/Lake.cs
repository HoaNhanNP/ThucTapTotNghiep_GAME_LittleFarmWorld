using UnityEngine;

public class Lake : MonoBehaviour
{
    public int waterAmount = 50; // Số nước thu được mỗi lần

    void Awake()
    {
        gameObject.tag = "Lake"; // Gắn tag để Player_E nhận diện
    }
}