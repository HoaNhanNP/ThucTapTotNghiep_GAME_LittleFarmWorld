using UnityEngine;

public class SellTruck : MonoBehaviour
{
    private float sellTimer = 60f; // 1 phút = 60 giây
    private bool hasItem = false;
    private string itemToSell = "";
    private int itemQuantity = 0;

    void Awake()
    {
        gameObject.tag = "SellTruck"; // Gắn tag để Player_E nhận diện
    }

    void Update()
    {
        if (hasItem)
        {
            sellTimer -= Time.deltaTime;
            if (sellTimer <= 0)
            {
                SellItem();
                ResetTruck();
            }
        }
    }

    public void PlaceItem(string itemName, int quantity)
    {
        itemToSell = itemName;
        itemQuantity = quantity;
        hasItem = true;
        sellTimer = 60f; // Reset timer
        Debug.Log($"Đã đặt {quantity} {itemName} lên xe bán hàng. Chờ 1 phút để bán!");
    }

    private void SellItem()
    {
        ShopManager shop = FindFirstObjectByType<ShopManager>();
        if (shop != null)
        {
            int revenue = 0;
            switch (itemToSell.ToLower())
            {
                case "lúa": revenue = itemQuantity * 10; break;
                case "ngô": revenue = itemQuantity * 20; break;
                case "bí đỏ": revenue = itemQuantity * 30; break;
                case "trứng": revenue = itemQuantity * 15; break;
                case "thịt heo": revenue = itemQuantity * 20; break;
            }
            shop.money += revenue;
            Debug.Log($"Đã bán {itemQuantity} {itemToSell} và thu được ${revenue}!");
        }
    }

    private void ResetTruck()
    {
        hasItem = false;
        itemToSell = "";
        itemQuantity = 0;
    }
}