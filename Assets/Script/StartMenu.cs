using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;

    void Start()
    {
        // Kiểm tra null trước khi gán sự kiện
        if (continueButton == null)
        {
            Debug.LogError("ContinueButton chưa được gán trong Inspector!");
            return;
        }
        if (newGameButton == null)
        {
            Debug.LogError("NewGameButton chưa được gán trong Inspector!");
            return;
        }

        continueButton.onClick.AddListener(OnContinueButtonClick);
        newGameButton.onClick.AddListener(OnNewGameButtonClick);

        continueButton.interactable = PlayerPrefs.HasKey("Money");
    }

    void OnContinueButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    void OnNewGameButtonClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }
}