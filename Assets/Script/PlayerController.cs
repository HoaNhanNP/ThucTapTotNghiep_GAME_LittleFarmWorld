using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    private Vector3 velocity;

    private const string SPEED_PARAM = "Speed";
    private const string JUMP_PARAM = "Jump";

    public Camera Camera3;
    public Camera Camera1;
    public bool isFirstPerson = false;

    void Start()
    {
        Debug.Log("PlayerController Start");
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("Thiếu CharacterController!");
            enabled = false;
            return;
        }
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogWarning("Thiếu Animator!");
        Camera3.enabled = true;
        Camera1.enabled = false;
    }

    void Update()
    {
        Debug.Log("PlayerController Update");
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            Camera3.enabled = !isFirstPerson;
            Camera1.enabled = isFirstPerson;
        }

        if (controller == null) return;

        if (MouseCursorController.Instance != null && MouseCursorController.Instance.IsMouseLookEnabled())
        {
            HandleMovement();
        }
        else
        {
            if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void HandleMovement()
    {
        Debug.Log("HandleMovement");
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;
        float moveSpeed = move.magnitude;

        if (controller.isGrounded)
        {
            if (velocity.y < 0) velocity.y = -2f;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                if (animator != null) { /* animator.SetTrigger(JUMP_PARAM); */ }
            }
        }
        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = move * speed + velocity;
        controller.Move(finalMove * Time.deltaTime);

        if (animator != null) animator.SetFloat(SPEED_PARAM, moveSpeed);
    }
}