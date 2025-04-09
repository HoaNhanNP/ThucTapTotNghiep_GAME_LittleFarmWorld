using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public enum QuestType { PlantRice, PlantCorn, PlantPumpkin, Harvest }
    private QuestType currentQuest;
    private int progress = 0;
    private int goal = 10;
    private bool isQuestActive = false;

    public ShopManager shopManager;

    void Awake()
    {
        Debug.Log("QuestManager Awake() được gọi.");
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("QuestManager.Instance được gán.");
        }
        else
        {
            Debug.LogWarning("Đã có QuestManager.Instance, hủy GameObject này.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("QuestManager Start() được gọi.");
        if (shopManager == null)
        {
            shopManager = FindFirstObjectByType<ShopManager>();
            if (shopManager == null)
            {
                Debug.LogError("ShopManager chưa được gán trong QuestManager! Vui lòng kiểm tra.");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isQuestActive)
        {
            Debug.Log("Phím Q được nhấn, tạo quest mới.");
            GenerateRandomQuest();
        }
    }

    public void GenerateRandomQuest()
    {
        Debug.Log("GenerateRandomQuest được gọi.");
        int randomQuest = Random.Range(0, 4);
        currentQuest = (QuestType)randomQuest;
        progress = 0;

        string questDescription = "";
        switch (currentQuest)
        {
            case QuestType.PlantRice:
                questDescription = "Trồng 10 lúa - Thưởng: $20";
                break;
            case QuestType.PlantCorn:
                questDescription = "Trồng 10 ngô - Thưởng: $30";
                break;
            case QuestType.PlantPumpkin:
                questDescription = "Trồng 10 bí - Thưởng: $40";
                break;
            case QuestType.Harvest:
                questDescription = "Thu hoạch 10 lần - Thưởng: $50";
                break;
        }
        UIManager.Instance.ShowQuest(questDescription);
    }

    public void OnPlant(string plantType)
    {
        if (!isQuestActive) return;

        switch (currentQuest)
        {
            case QuestType.PlantRice:
                if (plantType == "Rice") progress++;
                break;
            case QuestType.PlantCorn:
                if (plantType == "Corn") progress++;
                break;
            case QuestType.PlantPumpkin:
                if (plantType == "Pumpkin") progress++;
                break;
        }
        Debug.Log($"Quest progress: {progress}/{goal} (Planting {plantType})");
        CheckQuestCompletion();
    }

    public void OnHarvest()
    {
        if (!isQuestActive) return;

        if (currentQuest == QuestType.Harvest) progress++;
        Debug.Log($"Quest progress: {progress}/{goal} (Harvesting)");
        CheckQuestCompletion();
    }

    private void CheckQuestCompletion()
    {
        if (progress >= goal)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        if (shopManager == null)
        {
            Debug.LogError("ShopManager không tồn tại, không thể cộng tiền thưởng!");
            return;
        }

        switch (currentQuest)
        {
            case QuestType.PlantRice:
                shopManager.money += 20;
                break;
            case QuestType.PlantCorn:
                shopManager.money += 30;
                break;
            case QuestType.PlantPumpkin:
                shopManager.money += 40;
                break;
            case QuestType.Harvest:
                shopManager.money += 50;
                break;
        }
        isQuestActive = false;
        Debug.Log("Quest hoàn thành! Nhấn Q để nhận quest mới.");
    }

    public void AcceptQuest()
    {
        Debug.Log("AcceptQuest được gọi.");
        isQuestActive = true;
        progress = 0;
        Debug.Log("Quest đã được nhận!");
        UIManager.Instance.CloseQuestPanel();
    }

    public QuestType GetCurrentQuestType() { return currentQuest; }
    public int GetQuestProgress() { return progress; }
    public bool IsQuestActive() { return isQuestActive; }

    public void LoadQuest(int questType, int questProgress, bool active)
    {
        Debug.Log("LoadQuest được gọi.");
        currentQuest = (QuestType)questType;
        progress = questProgress;
        isQuestActive = active;
        if (isQuestActive)
        {
            Debug.Log("Đã tải lại quest: " + currentQuest + " với tiến độ " + progress);
            string questDescription = "";
            switch (currentQuest)
            {
                case QuestType.PlantRice:
                    questDescription = "Trồng 10 lúa - Thưởng: $20";
                    break;
                case QuestType.PlantCorn:
                    questDescription = "Trồng 10 ngô - Thưởng: $30";
                    break;
                case QuestType.PlantPumpkin:
                    questDescription = "Trồng 10 bí - Thưởng: $40";
                    break;
                case QuestType.Harvest:
                    questDescription = "Thu hoạch 10 lần - Thưởng: $50";
                    break;
            }
            UIManager.Instance.ShowQuest(questDescription);
        }
    }
}