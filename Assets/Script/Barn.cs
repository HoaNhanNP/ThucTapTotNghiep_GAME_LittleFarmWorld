using UnityEngine;

public class Barn : MonoBehaviour
{
    public string barnType; // "Chicken" hoặc "Pig" (gán trong Inspector)
    public float feedTime;  // Thời gian chờ sau khi cho ăn (Chicken: 20s, Pig: 30s)
    private float currentFeedTime; // Thời gian còn lại
    private bool isFed = false;    // Đã cho ăn chưa
    private bool canHarvest = false; // Có thể thu hoạch không
    public InventoryManager inventoryManager; // Tham chiếu kho đồ

    void Start()
    {
        // Gán thời gian chờ dựa trên loại chuồng
        if (gameObject.CompareTag("ChickenBarn"))
        {
            barnType = "Chicken";
            feedTime = 20f;
        }
        else if (gameObject.CompareTag("PigBarn"))
        {
            barnType = "Pig";
            feedTime = 30f;
        }

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager chưa được gán trong Barn!");
        }
    }

    void Update()
    {
        if (isFed && currentFeedTime > 0)
        {
            currentFeedTime -= Time.deltaTime;
            if (currentFeedTime <= 0)
            {
                canHarvest = true;
                isFed = false; // Reset sau khi sẵn sàng thu hoạch
            }
        }
    }

    public void Feed()
    {
        if (!isFed && !canHarvest) // Chỉ cho ăn nếu chưa ăn và chưa sẵn sàng thu hoạch
        {
            if (barnType == "Chicken" && inventoryManager.rice >= 3)
            {
                inventoryManager.AddItem("lúa", -3); // Trừ 3 lúa
                StartFeeding();
            }
            else if (barnType == "Pig" && inventoryManager.pumpkin >= 3)
            {
                inventoryManager.AddItem("bí đỏ", -3); // Trừ 3 bí đỏ
                StartFeeding();
            }
            else
            {
                Debug.Log("Không đủ nguyên liệu để cho " + barnType + " ăn!");
            }
        }
    }

    public void Harvest()
    {
        if (canHarvest)
        {
            if (barnType == "Chicken")
            {
                inventoryManager.AddItem("trứng", 5); // +5 trứng
            }
            else if (barnType == "Pig")
            {
                inventoryManager.AddItem("thịt heo", 5); // +5 thịt heo
            }
            canHarvest = false; // Reset sau khi thu hoạch
        }
    }

    private void StartFeeding()
    {
        isFed = true;
        currentFeedTime = feedTime;
    }

    public bool CanHarvest()
    {
        return canHarvest;
    }
}