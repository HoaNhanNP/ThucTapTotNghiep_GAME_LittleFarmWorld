using UnityEngine;

public class Field : MonoBehaviour
{
    public bool[] isPlanted;        // Trạng thái trồng cho 4 ô
    public string[] plantType;      // Loại cây cho 4 ô
    public float[] growthTime;      // Thời gian trưởng thành cho 4 ô
    public bool[] isMature;         // Trạng thái trưởng thành cho 4 ô

    // Tham chiếu tới InventoryManager
    public InventoryManager inventoryManager; // Gán trong Inspector

    public GameObject sproutModel;  // Mô hình mầm cây
    public GameObject riceModel;    // Mô hình lúa
    public GameObject cornModel;    // Mô hình ngô
    public GameObject pumpkinModel; // Mô hình bí

    // Độ cao tùy chỉnh cho từng loại cây
    public float sproutHeight = 0.5f;  // Độ cao mầm cây
    public float riceHeight = 1f;      // Độ cao lúa
    public float cornHeight = 1f;      // Độ cao ngô
    public float pumpkinHeight = 0.5f; // Độ cao bí

    private GameObject[] currentPlants; // Lưu 4 mô hình hiện tại

    void Awake()
    {
        // Khởi tạo mảng với kích thước 4 nếu chưa được gán
        if (isPlanted == null || isPlanted.Length != 4) isPlanted = new bool[4];
        if (plantType == null || plantType.Length != 4) plantType = new string[4];
        if (growthTime == null || growthTime.Length != 4) growthTime = new float[4];
        if (isMature == null || isMature.Length != 4) isMature = new bool[4];
        if (currentPlants == null || currentPlants.Length != 4) currentPlants = new GameObject[4];

        // Kiểm tra xem InventoryManager đã được gán chưa
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager chưa được gán trong Field!");
        }

        // Kiểm tra các mô hình
        if (sproutModel == null) Debug.LogError("sproutModel chưa được gán trong Inspector!");
        if (riceModel == null) Debug.LogError("riceModel chưa được gán trong Inspector!");
        if (cornModel == null) Debug.LogError("cornModel chưa được gán trong Inspector!");
        if (pumpkinModel == null) Debug.LogError("pumpkinModel chưa được gán trong Inspector!");
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (isPlanted[i] && growthTime[i] > 0)
            {
                growthTime[i] -= Time.deltaTime;
                if (growthTime[i] <= 0)
                {
                    isMature[i] = true;
                    ReplaceModel(i);
                }
            }
        }
    }

    public void Plant(string type)
    {
        // Kiểm tra InventoryManager
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager không tồn tại, không thể trồng!");
            return;
        }

        // Kiểm tra số lượng hạt giống trong kho trước khi trồng
        int seedCount = GetSeedCount(type);
        if (seedCount <= 0)
        {
            Debug.Log("Không đủ hạt giống để trồng " + type);
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (!isPlanted[i]) // Trồng vào ô trống đầu tiên
            {
                isPlanted[i] = true;
                plantType[i] = type;

                switch (type)
                {
                    case "Rice":
                        growthTime[i] = 10f;
                        inventoryManager.AddItem("lúa", -1); // Trừ 1 hạt lúa từ kho
                        break;
                    case "Corn":
                        growthTime[i] = 15f;
                        inventoryManager.AddItem("ngô", -1); // Trừ 1 hạt ngô từ kho
                        break;
                    case "Pumpkin":
                        growthTime[i] = 20f;
                        inventoryManager.AddItem("bí đỏ", -1); // Trừ 1 hạt bí từ kho
                        break;
                }

                if (sproutModel != null)
                {
                    Vector3 offset = GetOffset(i);
                    currentPlants[i] = Instantiate(sproutModel, transform.position + offset, Quaternion.identity, transform);
                    AdjustHeight(currentPlants[i], sproutHeight); // Dùng độ cao của mầm
                }
                else
                {
                    Debug.LogError("sproutModel chưa được gán trong Inspector!");
                    return;
                }

                // Thông báo cho QuestManager rằng người chơi vừa trồng cây
                QuestManager.Instance.OnPlant(type);
                break; // Chỉ trồng 1 cây mỗi lần
            }
        }
    }

    public void Harvest()
    {
        // Kiểm tra InventoryManager
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager không tồn tại, không thể thu hoạch!");
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (isMature[i])
            {
                switch (plantType[i])
                {
                    case "Rice":
                        inventoryManager.AddItem("lúa", 3); // Thu hoạch +3 lúa vào kho
                        break;
                    case "Corn":
                        inventoryManager.AddItem("ngô", 3); // Thu hoạch +3 ngô vào kho
                        break;
                    case "Pumpkin":
                        inventoryManager.AddItem("bí đỏ", 3); // Thu hoạch +3 bí vào kho
                        break;
                }

                Destroy(currentPlants[i]);
                isPlanted[i] = false;
                isMature[i] = false;
                plantType[i] = "";
                growthTime[i] = 0f;

                // Thông báo cho QuestManager rằng người chơi vừa thu hoạch
                QuestManager.Instance.OnHarvest();
            }
        }
    }

    private void ReplaceModel(int index)
    {
        if (currentPlants[index] != null)
        {
            Destroy(currentPlants[index]);
        }

        Vector3 offset = GetOffset(index);
        switch (plantType[index])
        {
            case "Rice":
                if (riceModel != null)
                {
                    currentPlants[index] = Instantiate(riceModel, transform.position + offset, Quaternion.identity, transform);
                    AdjustHeight(currentPlants[index], riceHeight); // Dùng độ cao của lúa
                }
                break;
            case "Corn":
                if (cornModel != null)
                {
                    currentPlants[index] = Instantiate(cornModel, transform.position + offset, Quaternion.identity, transform);
                    AdjustHeight(currentPlants[index], cornHeight); // Dùng độ cao của ngô
                }
                break;
            case "Pumpkin":
                if (pumpkinModel != null)
                {
                    currentPlants[index] = Instantiate(pumpkinModel, transform.position + offset, Quaternion.identity, transform);
                    AdjustHeight(currentPlants[index], pumpkinHeight); // Dùng độ cao của bí
                }
                break;
        }
    }

    private Vector3 GetOffset(int index)
    {
        switch (index)
        {
            case 0: return new Vector3(-0.25f, 0, -0.25f); // Góc dưới trái
            case 1: return new Vector3(0.25f, 0, -0.25f);  // Góc dưới phải
            case 2: return new Vector3(-0.25f, 0, 0.25f);  // Góc trên trái
            case 3: return new Vector3(0.25f, 0, 0.25f);   // Góc trên phải
            default: return Vector3.zero;
        }
    }

    private void AdjustHeight(GameObject plant, float height)
    {
        if (plant != null)
        {
            Vector3 pos = plant.transform.localPosition;
            pos.y = height; // Sử dụng độ cao tùy chỉnh
            plant.transform.localPosition = pos;
        }
    }

    private int GetSeedCount(string type)
    {
        if (inventoryManager == null) return 0;

        switch (type)
        {
            case "Rice": return inventoryManager.rice;
            case "Corn": return inventoryManager.corn;
            case "Pumpkin": return inventoryManager.pumpkin;
            default: return 0;
        }
    }
}