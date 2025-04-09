using UnityEngine;

public class MouseCursorController : MonoBehaviour
{
    public static MouseCursorController Instance;

    private bool isCursorVisible = false;
    private bool isMouseLookEnabled = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            ShowCursor(true);
            EnableMouseLook(false);
        }
        else if (!IsInteracting())
        {
            ShowCursor(false);
            EnableMouseLook(true);
        }
    }

    public void ShowCursor(bool show)
    {
        if (show != isCursorVisible)
        {
            isCursorVisible = show;
            Cursor.visible = show;
            Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void EnableMouseLook(bool enable)
    {
        isMouseLookEnabled = enable;
    }

    public bool IsMouseLookEnabled()
    {
        return isMouseLookEnabled;
    }

    private bool IsInteracting()
    {
        return UIManager.Instance != null && (UIManager.Instance.actionMenu.activeSelf || UIManager.Instance.seedMenu.activeSelf);
    }
}