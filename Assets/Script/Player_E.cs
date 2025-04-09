using UnityEngine;

public class Player_E : MonoBehaviour
{
    public float interactRange = 1.5f;
    private Field currentField;
    private Barn currentBarn;
    private SellTruck currentSellTruck; // Thêm xe bán hàng
    private bool isInteracting = false;
    private CameraController cameraController;

    void Start()
    {
        cameraController = GetComponentInChildren<CameraController>();
        if (cameraController == null)
        {
            Debug.LogError("Không tìm thấy CameraController trong nhân vật!");
        }
    }

    void Update()
    {
        CheckNearbyObject();
        if ((currentField != null || currentBarn != null || currentSellTruck != null) && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            isInteracting = true;
            if (currentField != null)
            {
                UIManager.Instance.ShowActionMenu(currentField);
            }
            else if (currentBarn != null)
            {
                UIManager.Instance.ShowBarnMenu(currentBarn);
            }
            else if (currentSellTruck != null)
            {
                UIManager.Instance.ShowSellMenu(currentSellTruck);
            }
            UIManager.Instance.ShowInteractButton(false);
            StartInteraction();
        }
    }

    private void CheckNearbyObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRange);
        currentField = null;
        currentBarn = null;
        currentSellTruck = null;

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Field"))
            {
                currentField = hit.GetComponent<Field>();
                if (!isInteracting)
                {
                    UIManager.Instance.ShowInteractButton(true);
                }
                return;
            }
            else if (hit.CompareTag("ChickenBarn") || hit.CompareTag("PigBarn"))
            {
                currentBarn = hit.GetComponent<Barn>();
                if (!isInteracting)
                {
                    UIManager.Instance.ShowInteractButton(true);
                }
                return;
            }
            else if (hit.CompareTag("SellTruck"))
            {
                currentSellTruck = hit.GetComponent<SellTruck>();
                if (!isInteracting)
                {
                    UIManager.Instance.ShowInteractButton(true);
                }
                return;
            }
        }
        if (!isInteracting)
        {
            UIManager.Instance.ShowInteractButton(false);
        }
    }

    public void StartInteraction()
    {
        isInteracting = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (cameraController != null)
        {
            cameraController.LockCamera(true);
        }
    }

    public void ResetInteraction()
    {
        isInteracting = false;
        currentField = null;
        currentBarn = null;
        currentSellTruck = null;
        if (cameraController != null)
        {
            cameraController.LockCamera(false);
        }
    }
}