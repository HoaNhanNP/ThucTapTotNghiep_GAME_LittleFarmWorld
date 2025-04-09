using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Button inventoryButton;
    public Button closeButton;
    private bool isInventoryOpen = false;

    public int rice = 0;
    public int pumpkin = 0;
    public int corn = 0;
    public int eggs = 0;
    public int pork = 0;

    public Text riceText;
    public Text pumpkinText;
    public Text cornText;
    public Text eggsText;
    public Text porkText;

    void Start()
    {
        // Kiểm tra null
        if (inventoryPanel == null) { Debug.LogError("InventoryPanel chưa được gán!"); return; }
        if (inventoryButton == null) { Debug.LogError("InventoryButton chưa được gán!"); return; }
        if (closeButton == null) { Debug.LogError("CloseButton chưa được gán!"); return; }
        if (riceText == null) { Debug.LogError("RiceText chưa được gán!"); return; }
        if (pumpkinText == null) { Debug.LogError("PumpkinText chưa được gán!"); return; }
        if (cornText == null) { Debug.LogError("CornText chưa được gán!"); return; }
        if (eggsText == null) { Debug.LogError("EggsText chưa được gán!"); return; }
        if (porkText == null) { Debug.LogError("PorkText chưa được gán!"); return; }

        // Ẩn inventory panel khi bắt đầu
        inventoryPanel.SetActive(false);
        inventoryButton.onClick.AddListener(ToggleInventory);
        closeButton.onClick.AddListener(CloseInventory);
        UpdateInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        if (inventoryPanel != null) inventoryPanel.SetActive(isInventoryOpen);
    }

    void CloseInventory()
    {
        isInventoryOpen = false;
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
    }

    public void UpdateInventoryUI()
    {
        if (riceText != null) riceText.text = "Lúa: " + rice;
        if (pumpkinText != null) pumpkinText.text = "Bí đỏ: " + pumpkin;
        if (cornText != null) cornText.text = "Ngô: " + corn;
        if (eggsText != null) eggsText.text = "Trứng: " + eggs;
        if (porkText != null) porkText.text = "Thịt heo: " + pork;
    }

    public void AddItem(string itemName, int amount)
    {
        switch (itemName.ToLower())
        {
            case "lúa": rice += amount; break;
            case "bí đỏ": pumpkin += amount; break;
            case "ngô": corn += amount; break;
            case "trứng": eggs += amount; break;
            case "thịt heo": pork += amount; break;
            default: Debug.Log("Vật phẩm không tồn tại: " + itemName); break;
        }
        UpdateInventoryUI();
    }
}