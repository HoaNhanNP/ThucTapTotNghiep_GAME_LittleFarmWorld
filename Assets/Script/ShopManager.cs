using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public int money = 500; // Tiền ban đầu
    public InventoryManager inventoryManager; // Tham chiếu kho đồ

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager chưa được gán trong ShopManager!");
        }
    }

    public bool BuyItem(string itemName, int cost)
    {
        if (money >= cost)
        {
            money -= cost;
            inventoryManager.AddItem(itemName, 1); // Thêm 1 đơn vị vào kho
            return true;
        }
        else
        {
            Debug.Log("Không đủ tiền để mua " + itemName + "!");
            return false;
        }
    }
}