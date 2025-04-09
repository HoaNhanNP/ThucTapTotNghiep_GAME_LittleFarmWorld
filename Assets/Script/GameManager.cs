using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ShopManager shopManager;
    public QuestManager questManager;

    void Start()
    {
        // Kiểm tra xem có dữ liệu lưu không
        if (PlayerPrefs.HasKey("Money"))
        {
            LoadGame();
        }
    }

    void LoadGame()
    {
        // Load tiền
        shopManager.money = PlayerPrefs.GetInt("Money", 0);

        // Load kho đồ
        inventoryManager.rice = PlayerPrefs.GetInt("Rice", 0);
        inventoryManager.corn = PlayerPrefs.GetInt("Corn", 0);
        inventoryManager.pumpkin = PlayerPrefs.GetInt("Pumpkin", 0);
        inventoryManager.eggs = PlayerPrefs.GetInt("Eggs", 0);
        inventoryManager.pork = PlayerPrefs.GetInt("Pork", 0);
        inventoryManager.UpdateInventoryUI();

        // Load quest
        int questType = PlayerPrefs.GetInt("QuestType", 0);
        int questProgress = PlayerPrefs.GetInt("QuestProgress", 0);
        bool questActive = PlayerPrefs.GetInt("QuestActive", 0) == 1;
        questManager.LoadQuest(questType, questProgress, questActive);

        Debug.Log("Đã tải tiến độ!");
    }
}