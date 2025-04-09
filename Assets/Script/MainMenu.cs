using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        // Chuyển sang Scene game chính (thay "GameScene" bằng tên Scene game của mày)
        SceneManager.LoadScene("Main");
    }

    // public void Continue()
    // {
    //     // Logic để tiếp tục game, ví dụ load dữ liệu đã lưu (nếu có)
    //     Debug.Log("Tiếp tục game - Chưa có dữ liệu lưu!");
    // }

    public void Exit()
    {
        // Thoát game
        Application.Quit();
        Debug.Log("Thoát game"); // Để kiểm tra trong Editor
    }
}