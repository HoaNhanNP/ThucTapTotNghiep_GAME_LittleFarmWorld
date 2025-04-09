using UnityEngine;
using UnityEngine.UI;

public class FieldInteraction : MonoBehaviour
{
    public GameObject riceOption; // UI cho lựa chọn "Lúa"
    public GameObject cornOption; // UI cho lựa chọn "Ngô"
    public GameObject pumpkinOption; // UI cho lựa chọn "Bí"
    public GameObject clockIcon; // Icon đồng hồ

    private bool isNearField = false;
    private GameObject currentField;

    void Start()
    {
        // Khởi tạo trạng thái ban đầu
        if (riceOption != null)
            riceOption.SetActive(false);
        else
            Debug.LogError("riceOption chưa được gắn!");

        if (cornOption != null)
            cornOption.SetActive(false);
        else
            Debug.LogError("cornOption chưa được gắn!");

        if (pumpkinOption != null)
            pumpkinOption.SetActive(false);
        else
            Debug.LogError("pumpkinOption chưa được gắn!");

        if (clockIcon != null)
            clockIcon.SetActive(false);
        else
            Debug.LogError("clockIcon chưa được gắn!");
    }

    void Update()
    {
        // Kiểm tra phím 1, 2, 3 khi ở gần thửa ruộng
        if (isNearField)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Phím 1 cho Lúa
                PlantCrop("Rice");
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // Phím 2 cho Ngô
                PlantCrop("Corn");
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // Phím 3 cho Bí
                PlantCrop("Pumpkin");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Khi nhân vật vào vùng thửa ruộng
        if (other.CompareTag("Field"))
        {
            isNearField = true;
            currentField = other.gameObject;
            ShowOptions(true); // Hiện 3 lựa chọn
            if (clockIcon != null)
                clockIcon.SetActive(false); // Đảm bảo đồng hồ ẩn
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Khi nhân vật rời khỏi thửa ruộng
        if (other.CompareTag("Field"))
        {
            isNearField = false;
            currentField = null;
            ShowOptions(false); // Ẩn 3 lựa chọn
            // Không ẩn đồng hồ ở đây vì đồng hồ liên quan đến quá trình trồng
        }
    }

    void ShowOptions(bool show)
    {
        // Bật/tắt 3 lựa chọn
        if (riceOption != null)
            riceOption.SetActive(show);
        if (cornOption != null)
            cornOption.SetActive(show);
        if (pumpkinOption != null)
            pumpkinOption.SetActive(show);
    }

    void PlantCrop(string seedType)
    {
        if (currentField != null)
        {
            Debug.Log("Trồng " + seedType + " trên " + currentField.name);

            // Tạo cây tương ứng
            GameObject cropPrefab = null;
            switch (seedType)
            {
                case "Rice": cropPrefab = Resources.Load<GameObject>("RiceCrop"); break;
                case "Corn": cropPrefab = Resources.Load<GameObject>("CornCrop"); break;
                case "Pumpkin": cropPrefab = Resources.Load<GameObject>("PumpkinCrop"); break;
            }

            if (cropPrefab != null)
            {
                GameObject crop = Instantiate(cropPrefab, currentField.transform.position, Quaternion.identity);
                crop.transform.parent = currentField.transform;

                // Hiện đồng hồ và ẩn lựa chọn
                ShowOptions(false);
                if (clockIcon != null)
                    clockIcon.SetActive(true);

                // Reset sau 3 giây
                Invoke("ResetUI", 3f);
            }
            else
            {
                Debug.LogError("Không tìm thấy prefab cho " + seedType + " trong Resources!");
            }
        }
    }

    void ResetUI()
    {
        // Reset giao diện sau khi trồng xong
        if (isNearField)
            ShowOptions(true); // Hiện lại 3 lựa chọn nếu vẫn gần thửa ruộng
        if (clockIcon != null)
            clockIcon.SetActive(false); // Ẩn đồng hồ
    }
}