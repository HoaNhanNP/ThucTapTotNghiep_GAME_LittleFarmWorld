using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject interactButton;
    public GameObject actionMenu;
    public Button plantButton;
    public Button harvestButton;
    public GameObject seedMenu;
    public Button riceButton;
    public Button cornButton;
    public Button pumpkinButton;
    public Button closeButton;
    public Text riceCountText;
    public Text cornCountText;
    public Text pumpkinCountText;

    public GameObject barnMenu;
    public Button feedButton;
    public Button barnHarvestButton;

    public GameObject shopMenu;
    public Button shopButton;
    public Button buyRiceButton;
    public Button buyPumpkinButton;
    public Button buyCornButton;
    public Button shopCloseButton;
    public Text moneyText;

    public GameObject questPanel;
    public Text questText;
    public Button acceptQuestButton;

    public GameObject sellMenu;
    public Button sellRiceButton;
    public Button sellCornButton;
    public Button sellPumpkinButton;
    public Button sellEggsButton;
    public Button sellPorkButton;
    public Button sellCloseButton;
    public Text sellRiceText;
    public Text sellCornText;
    public Text sellPumpkinText;
    public Text sellEggsText;
    public Text sellPorkText;

    public GameObject savePanel;
    public Button saveYesButton;
    public Button saveNoButton;
    public Button homeButton;

    private Field currentField;
    private Barn currentBarn;
    private SellTruck currentSellTruck;
    public InventoryManager inventoryManager;
    public ShopManager shopManager;

    void Awake()
    {
        Debug.Log("UIManager Awake() được gọi.");
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("UIManager Start() được gọi.");
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager chưa được gán trong UIManager!");
            return;
        }
        if (shopManager == null)
        {
            Debug.LogError("ShopManager chưa được gán trong UIManager!");
            return;
        }

        if (!CheckUIElements()) return;

        riceButton.onClick.AddListener(OnRiceButtonClick);
        cornButton.onClick.AddListener(OnCornButtonClick);
        pumpkinButton.onClick.AddListener(OnPumpkinButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
        plantButton.onClick.AddListener(OnPlantButtonClick);
        harvestButton.onClick.AddListener(OnHarvestButtonClick);

        feedButton.onClick.AddListener(OnFeedButtonClick);
        barnHarvestButton.onClick.AddListener(OnBarnHarvestButtonClick);

        shopButton.onClick.AddListener(OnShopButtonClick);
        buyRiceButton.onClick.AddListener(() => OnBuyItemClick("lúa", 10));
        buyPumpkinButton.onClick.AddListener(() => OnBuyItemClick("bí đỏ", 20));
        buyCornButton.onClick.AddListener(() => OnBuyItemClick("ngô", 30));
        shopCloseButton.onClick.AddListener(OnShopCloseButtonClick);

        if (acceptQuestButton != null)
        {
            Debug.Log("Gán sự kiện onClick cho acceptQuestButton.");
            acceptQuestButton.onClick.RemoveAllListeners(); // Xóa các listener cũ để tránh trùng lặp
            acceptQuestButton.onClick.AddListener(OnAcceptQuestButtonClick);
        }
        else
        {
            Debug.LogError("acceptQuestButton chưa được gán trong UIManager!");
        }

        sellRiceButton.onClick.AddListener(() => OnSellItemClick("lúa", 1));
        sellCornButton.onClick.AddListener(() => OnSellItemClick("ngô", 1));
        sellPumpkinButton.onClick.AddListener(() => OnSellItemClick("bí đỏ", 1));
        sellEggsButton.onClick.AddListener(() => OnSellItemClick("trứng", 1));
        sellPorkButton.onClick.AddListener(() => OnSellItemClick("thịt heo", 1));
        sellCloseButton.onClick.AddListener(OnCloseButtonClick);

        homeButton.onClick.AddListener(OnHomeButtonClick);
        saveYesButton.onClick.AddListener(OnSaveYesButtonClick);
        saveNoButton.onClick.AddListener(OnSaveNoButtonClick);

        HideAllMenus();
    }

    void Update()
    {
        UpdateMoneyUI();
    }

    private bool CheckUIElements()
    {
        if (interactButton == null) { Debug.LogError("InteractButton chưa được gán!"); return false; }
        if (actionMenu == null) { Debug.LogError("ActionMenu chưa được gán!"); return false; }
        if (plantButton == null) { Debug.LogError("PlantButton chưa được gán!"); return false; }
        if (harvestButton == null) { Debug.LogError("HarvestButton chưa được gán!"); return false; }
        if (seedMenu == null) { Debug.LogError("SeedMenu chưa được gán!"); return false; }
        if (riceButton == null) { Debug.LogError("RiceButton chưa được gán!"); return false; }
        if (cornButton == null) { Debug.LogError("CornButton chưa được gán!"); return false; }
        if (pumpkinButton == null) { Debug.LogError("PumpkinButton chưa được gán!"); return false; }
        if (closeButton == null) { Debug.LogError("CloseButton chưa được gán!"); return false; }
        if (riceCountText == null) { Debug.LogError("RiceCountText chưa được gán!"); return false; }
        if (cornCountText == null) { Debug.LogError("CornCountText chưa được gán!"); return false; }
        if (pumpkinCountText == null) { Debug.LogError("PumpkinCountText chưa được gán!"); return false; }
        if (barnMenu == null) { Debug.LogError("BarnMenu chưa được gán!"); return false; }
        if (feedButton == null) { Debug.LogError("FeedButton chưa được gán!"); return false; }
        if (barnHarvestButton == null) { Debug.LogError("BarnHarvestButton chưa được gán!"); return false; }
        if (shopMenu == null) { Debug.LogError("ShopMenu chưa được gán!"); return false; }
        if (shopButton == null) { Debug.LogError("ShopButton chưa được gán!"); return false; }
        if (buyRiceButton == null) { Debug.LogError("BuyRiceButton chưa được gán!"); return false; }
        if (buyPumpkinButton == null) { Debug.LogError("BuyPumpkinButton chưa được gán!"); return false; }
        if (buyCornButton == null) { Debug.LogError("BuyCornButton chưa được gán!"); return false; }
        if (shopCloseButton == null) { Debug.LogError("ShopCloseButton chưa được gán!"); return false; }
        if (moneyText == null) { Debug.LogError("MoneyText chưa được gán!"); return false; }
        if (questPanel == null) { Debug.LogError("QuestPanel chưa được gán!"); return false; }
        if (questText == null) { Debug.LogError("QuestText chưa được gán!"); return false; }
        if (acceptQuestButton == null) { Debug.LogError("AcceptQuestButton chưa được gán!"); return false; }
        if (sellMenu == null) { Debug.LogError("SellMenu chưa được gán!"); return false; }
        if (sellRiceButton == null) { Debug.LogError("SellRiceButton chưa được gán!"); return false; }
        if (sellCornButton == null) { Debug.LogError("SellCornButton chưa được gán!"); return false; }
        if (sellPumpkinButton == null) { Debug.LogError("SellPumpkinButton chưa được gán!"); return false; }
        if (sellEggsButton == null) { Debug.LogError("SellEggsButton chưa được gán!"); return false; }
        if (sellPorkButton == null) { Debug.LogError("SellPorkButton chưa được gán!"); return false; }
        if (sellCloseButton == null) { Debug.LogError("SellCloseButton chưa được gán!"); return false; }
        if (sellRiceText == null) { Debug.LogError("SellRiceText chưa được gán!"); return false; }
        if (sellCornText == null) { Debug.LogError("SellCornText chưa được gán!"); return false; }
        if (sellPumpkinText == null) { Debug.LogError("SellPumpkinText chưa được gán!"); return false; }
        if (sellEggsText == null) { Debug.LogError("SellEggsText chưa được gán!"); return false; }
        if (sellPorkText == null) { Debug.LogError("SellPorkText chưa được gán!"); return false; }
        if (savePanel == null) { Debug.LogError("SavePanel chưa được gán!"); return false; }
        if (saveYesButton == null) { Debug.LogError("SaveYesButton chưa được gán!"); return false; }
        if (saveNoButton == null) { Debug.LogError("SaveNoButton chưa được gán!"); return false; }
        if (homeButton == null) { Debug.LogError("HomeButton chưa được gán!"); return false; }
        return true;
    }

    private void HideAllMenus()
    {
        if (interactButton != null) interactButton.SetActive(false);
        if (actionMenu != null) actionMenu.SetActive(false);
        if (seedMenu != null) seedMenu.SetActive(false);
        if (barnMenu != null) barnMenu.SetActive(false);
        if (shopMenu != null) shopMenu.SetActive(false);
        if (questPanel != null) questPanel.SetActive(false);
        if (sellMenu != null) sellMenu.SetActive(false);
        if (savePanel != null) savePanel.SetActive(false);
    }

    public void ShowInteractButton(bool show)
    {
        if (interactButton != null) interactButton.SetActive(show);
    }

    public void ShowActionMenu(Field field)
    {
        currentField = field;
        currentBarn = null;
        currentSellTruck = null;
        if (actionMenu != null) actionMenu.SetActive(true);

        bool allPlanted = true;
        for (int i = 0; i < 4; i++)
        {
            if (!field.isPlanted[i])
            {
                allPlanted = false;
                break;
            }
        }
        if (plantButton != null) plantButton.interactable = !allPlanted;

        bool hasMature = false;
        for (int i = 0; i < 4; i++)
        {
            if (field.isMature[i])
            {
                hasMature = true;
                break;
            }
        }
        if (harvestButton != null) harvestButton.interactable = hasMature;

        if (MouseCursorController.Instance != null)
        {
            MouseCursorController.Instance.ShowCursor(true);
            MouseCursorController.Instance.EnableMouseLook(false);
        }
        FindFirstObjectByType<Player_E>()?.StartInteraction();
    }

    public void ShowBarnMenu(Barn barn)
    {
        currentBarn = barn;
        currentField = null;
        currentSellTruck = null;
        if (barnMenu != null) barnMenu.SetActive(true);

        if (feedButton != null) feedButton.interactable = !barn.CanHarvest();
        if (barnHarvestButton != null) barnHarvestButton.interactable = barn.CanHarvest();

        if (MouseCursorController.Instance != null)
        {
            MouseCursorController.Instance.ShowCursor(true);
            MouseCursorController.Instance.EnableMouseLook(false);
        }
        FindFirstObjectByType<Player_E>()?.StartInteraction();
    }

    public void ShowSellMenu(SellTruck truck)
    {
        currentSellTruck = truck;
        currentField = null;
        currentBarn = null;
        if (sellMenu != null) sellMenu.SetActive(true);
        UpdateSellMenu();

        if (MouseCursorController.Instance != null)
        {
            MouseCursorController.Instance.ShowCursor(true);
            MouseCursorController.Instance.EnableMouseLook(false);
        }
        FindFirstObjectByType<Player_E>()?.StartInteraction();
    }

    public void OnShopButtonClick()
    {
        if (shopMenu != null) shopMenu.SetActive(true);
        if (MouseCursorController.Instance != null)
        {
            MouseCursorController.Instance.ShowCursor(true);
            MouseCursorController.Instance.EnableMouseLook(false);
        }
    }

    public void OnPlantButtonClick()
    {
        if (seedMenu != null) seedMenu.SetActive(true);
        UpdateSeedMenu();
    }

    public void OnHarvestButtonClick()
    {
        if (currentField != null)
        {
            currentField.Harvest();
            CloseMenus();
        }
        else
        {
            Debug.LogWarning("Không có thửa ruộng nào để thu hoạch!");
        }
    }

    public void OnRiceButtonClick()
    {
        if (inventoryManager.rice > 0)
        {
            currentField.Plant("Rice");
            CloseMenus();
        }
    }

    public void OnCornButtonClick()
    {
        if (inventoryManager.corn > 0)
        {
            currentField.Plant("Corn");
            CloseMenus();
        }
    }

    public void OnPumpkinButtonClick()
    {
        if (inventoryManager.pumpkin > 0)
        {
            currentField.Plant("Pumpkin");
            CloseMenus();
        }
    }

    public void OnFeedButtonClick()
    {
        if (currentBarn != null)
        {
            currentBarn.Feed();
            if (barnMenu != null) barnMenu.SetActive(false);
            Player_E player = FindFirstObjectByType<Player_E>();
            if (player != null)
            {
                player.ResetInteraction();
            }
            if (MouseCursorController.Instance != null && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
            {
                MouseCursorController.Instance.ShowCursor(false);
                MouseCursorController.Instance.EnableMouseLook(true);
            }
            UpdateBarnMenu();
        }
    }

    public void OnBarnHarvestButtonClick()
    {
        if (currentBarn != null)
        {
            currentBarn.Harvest();
            CloseMenus();
        }
        else
        {
            Debug.LogWarning("Không có chuồng nào để thu hoạch!");
        }
    }

    public void OnBuyItemClick(string itemName, int cost)
    {
        if (shopManager.BuyItem(itemName, cost))
        {
            UpdateMoneyUI();
        }
    }

    public void OnSellItemClick(string itemName, int quantity)
    {
        if (currentSellTruck != null)
        {
            switch (itemName.ToLower())
            {
                case "lúa":
                    if (inventoryManager.rice >= quantity)
                    {
                        inventoryManager.AddItem("lúa", -quantity);
                        currentSellTruck.PlaceItem("lúa", quantity);
                        CloseMenus();
                    }
                    break;
                case "ngô":
                    if (inventoryManager.corn >= quantity)
                    {
                        inventoryManager.AddItem("ngô", -quantity);
                        currentSellTruck.PlaceItem("ngô", quantity);
                        CloseMenus();
                    }
                    break;
                case "bí đỏ":
                    if (inventoryManager.pumpkin >= quantity)
                    {
                        inventoryManager.AddItem("bí đỏ", -quantity);
                        currentSellTruck.PlaceItem("bí đỏ", quantity);
                        CloseMenus();
                    }
                    break;
                case "trứng":
                    if (inventoryManager.eggs >= quantity)
                    {
                        inventoryManager.AddItem("trứng", -quantity);
                        currentSellTruck.PlaceItem("trứng", quantity);
                        CloseMenus();
                    }
                    break;
                case "thịt heo":
                    if (inventoryManager.pork >= quantity)
                    {
                        inventoryManager.AddItem("thịt heo", -quantity);
                        currentSellTruck.PlaceItem("thịt heo", quantity);
                        CloseMenus();
                    }
                    break;
            }
        }
    }

    public void OnCloseButtonClick()
    {
        CloseMenus();
    }

    public void OnShopCloseButtonClick()
    {
        if (shopMenu != null) shopMenu.SetActive(false);

        Player_E player = FindFirstObjectByType<Player_E>();
        if (player != null)
        {
            player.ResetInteraction();
        }

        if (MouseCursorController.Instance != null && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
        {
            MouseCursorController.Instance.ShowCursor(false);
            MouseCursorController.Instance.EnableMouseLook(true);
        }
    }

    public void OnAcceptQuestButtonClick()
    {
        Debug.Log("Nút Accept Quest được nhấn! (OnAcceptQuestButtonClick được gọi)");
        if (QuestManager.Instance != null)
        {
            Debug.Log("QuestManager.Instance tồn tại, gọi AcceptQuest().");
            QuestManager.Instance.AcceptQuest();
        }
        else
        {
            Debug.LogError("QuestManager.Instance không tồn tại!");
        }
    }

    public void OnHomeButtonClick()
    {
        if (savePanel != null) savePanel.SetActive(true);
        if (MouseCursorController.Instance != null)
        {
            MouseCursorController.Instance.ShowCursor(true);
            MouseCursorController.Instance.EnableMouseLook(false);
        }
        FindFirstObjectByType<Player_E>()?.StartInteraction();
    }

    public void OnSaveYesButtonClick()
    {
        SaveGame();
        SceneManager.LoadScene("StartMenu");
    }

    public void OnSaveNoButtonClick()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Money", shopManager.money);
        PlayerPrefs.SetInt("Rice", inventoryManager.rice);
        PlayerPrefs.SetInt("Corn", inventoryManager.corn);
        PlayerPrefs.SetInt("Pumpkin", inventoryManager.pumpkin);
        PlayerPrefs.SetInt("Eggs", inventoryManager.eggs);
        PlayerPrefs.SetInt("Pork", inventoryManager.pork);

        QuestManager questManager = FindFirstObjectByType<QuestManager>();
        if (questManager != null)
        {
            PlayerPrefs.SetInt("QuestType", (int)questManager.GetCurrentQuestType());
            PlayerPrefs.SetInt("QuestProgress", questManager.GetQuestProgress());
            PlayerPrefs.SetInt("QuestActive", questManager.IsQuestActive() ? 1 : 0);
        }

        PlayerPrefs.Save();
        Debug.Log("Đã lưu tiến độ!");
    }

    private void UpdateSeedMenu()
    {
        if (riceCountText != null) riceCountText.text = "Rice: " + inventoryManager.rice;
        if (cornCountText != null) cornCountText.text = "Corn: " + inventoryManager.corn;
        if (pumpkinCountText != null) pumpkinCountText.text = "Pumpkin: " + inventoryManager.pumpkin;
        if (riceButton != null) riceButton.interactable = inventoryManager.rice > 0;
        if (cornButton != null) cornButton.interactable = inventoryManager.corn > 0;
        if (pumpkinButton != null) pumpkinButton.interactable = inventoryManager.pumpkin > 0;
    }

    private void UpdateBarnMenu()
    {
        if (currentBarn != null)
        {
            if (feedButton != null) feedButton.interactable = !currentBarn.CanHarvest();
            if (barnHarvestButton != null) barnHarvestButton.interactable = currentBarn.CanHarvest();
        }
    }

    private void UpdateSellMenu()
    {
        if (sellRiceText != null) sellRiceText.text = "Lúa: " + inventoryManager.rice;
        if (sellCornText != null) sellCornText.text = "Ngô: " + inventoryManager.corn;
        if (sellPumpkinText != null) sellPumpkinText.text = "Bí: " + inventoryManager.pumpkin;
        if (sellEggsText != null) sellEggsText.text = "Trứng: " + inventoryManager.eggs;
        if (sellPorkText != null) sellPorkText.text = "Thịt: " + inventoryManager.pork;

        if (sellRiceButton != null) sellRiceButton.interactable = inventoryManager.rice > 0;
        if (sellCornButton != null) sellCornButton.interactable = inventoryManager.corn > 0;
        if (sellPumpkinButton != null) sellPumpkinButton.interactable = inventoryManager.pumpkin > 0;
        if (sellEggsButton != null) sellEggsButton.interactable = inventoryManager.eggs > 0;
        if (sellPorkButton != null) sellPorkButton.interactable = inventoryManager.pork > 0;
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null && shopManager != null)
        {
            moneyText.text = "Money: $" + shopManager.money;
        }
    }

    private void CloseMenus()
    {
        if (seedMenu != null) seedMenu.SetActive(false);
        if (actionMenu != null) actionMenu.SetActive(false);
        if (barnMenu != null) barnMenu.SetActive(false);
        if (shopMenu != null) shopMenu.SetActive(false);
        if (sellMenu != null) sellMenu.SetActive(false);
        if (savePanel != null) savePanel.SetActive(false);

        Player_E player = FindFirstObjectByType<Player_E>();
        if (player != null)
        {
            player.ResetInteraction();
        }

        if (MouseCursorController.Instance != null && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
        {
            MouseCursorController.Instance.ShowCursor(false);
            MouseCursorController.Instance.EnableMouseLook(true);
        }
    }

    public void ShowQuest(string questDescription)
    {
        Debug.Log("ShowQuest được gọi với mô tả: " + questDescription);
        if (questText != null) questText.text = questDescription;
        if (questPanel != null)
        {
            Debug.Log("Hiển thị questPanel.");
            questPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("QuestPanel không được gán trong UIManager!");
        }
    }

    public void CloseQuestPanel()
    {
        Debug.Log("CloseQuestPanel được gọi.");
        if (questPanel != null) questPanel.SetActive(false);
        else Debug.LogError("QuestPanel không được gán trong UIManager!");
    }
}