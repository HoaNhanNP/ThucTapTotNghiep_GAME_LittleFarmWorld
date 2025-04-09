using UnityEngine;

public class PigController : MonoBehaviour
{
    private Animator animator;
    private float idleTimer = 0f;
    private float runTimer = 0f;
    private bool isRunning = false;
    private Vector3 targetPosition;
    private float speed = 1f; // Tốc độ di chuyển khi chạy
    private float changeDirectionTimer = 0f;
    private float changeDirectionInterval = 2f; // Thay đổi hướng sau mỗi 2 giây khi chạy

    // Giới hạn khu vực di chuyển
    public Vector3 boundaryCenter = Vector3.zero; // Tâm khu vực
    public Vector3 boundarySize = new Vector3(20f, 1f, 20f); // Kích thước khu vực

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position; // Vị trí ban đầu
        PickNewTargetPosition(); // Chọn vị trí ngẫu nhiên ngay từ đầu
    }

    void Update()
    {
        // Quản lý thời gian Idle và Run
        if (!isRunning)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 15f) // Sau 15 giây Idle
            {
                isRunning = true;
                animator.SetBool("IsRunning", true);
                idleTimer = 0f;
                PickNewTargetPosition(); // Chọn vị trí ngẫu nhiên khi bắt đầu chạy
            }
        }
        else
        {
            runTimer += Time.deltaTime;
            if (runTimer >= 10f) // Sau 10 giây Run
            {
                isRunning = false;
                animator.SetBool("IsRunning", false);
                runTimer = 0f;
            }

            // Thay đổi hướng ngẫu nhiên khi chạy
            changeDirectionTimer += Time.deltaTime;
            if (changeDirectionTimer >= changeDirectionInterval)
            {
                PickNewTargetPosition(); // Chọn vị trí ngẫu nhiên mới
                changeDirectionTimer = 0f; // Reset timer
            }

            // Di chuyển về vị trí mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Xoay con gà theo hướng di chuyển
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Nếu đã đến gần vị trí mục tiêu, chọn vị trí mới ngay lập tức
            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                PickNewTargetPosition();
                changeDirectionTimer = 0f;
            }
        }
    }

    // Chọn một vị trí ngẫu nhiên trong khu vực giới hạn
    void PickNewTargetPosition()
    {
        float halfWidth = boundarySize.x / 2f;
        float halfDepth = boundarySize.z / 2f;

        // Chọn vị trí ngẫu nhiên trong khu vực
        float randomX = Random.Range(boundaryCenter.x - halfWidth, boundaryCenter.x + halfWidth);
        float randomZ = Random.Range(boundaryCenter.z - halfDepth, boundaryCenter.z + halfDepth);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);

        // Đảm bảo vị trí mới đủ xa để tạo cảm giác thay đổi hướng
        while (Vector3.Distance(targetPosition, transform.position) < 2f)
        {
            randomX = Random.Range(boundaryCenter.x - halfWidth, boundaryCenter.x + halfWidth);
            randomZ = Random.Range(boundaryCenter.z - halfDepth, boundaryCenter.z + halfDepth);
            targetPosition = new Vector3(randomX, transform.position.y, randomZ);
        }

        Debug.Log("New Target Position: " + targetPosition); // Debug để kiểm tra
    }

    // Vẽ khu vực giới hạn trong Editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundaryCenter, boundarySize);
    }
}