using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float fallMultiplier = 3f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Animator animator;

    private Vector2 input;
    private Vector3 moveDirection;

    private bool isGrounded;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;
    rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

    if (cameraTransform == null)
    {
        cameraTransform = Camera.main.transform;
    }
    }


    private void Update()
    {
        // Nhận WASD
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        CalculateMoveDirection();

        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        Move();
        ApplyFallGravity();
    }


    private void CalculateMoveDirection()
    {
        // Hướng trước và phải của Camera
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Không cho camera ảnh hưởng trục Y
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // WASD theo hướng Camera
        moveDirection =
            cameraForward * input.y +
            cameraRight * input.x;

        moveDirection =
            Vector3.ClampMagnitude(moveDirection, 1f);
    }


    private void Move()
    {
        Vector3 velocity = rb.velocity;

        // Chỉ thay đổi X/Z
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        // Giữ nguyên Y để Gravity hoạt động
        rb.velocity = velocity;


        // Xoay Player theo hướng chạy
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    10f * Time.fixedDeltaTime
                )
            );
        }
    }


    private void UpdateAnimation()
    {
        float speed = moveDirection.magnitude;

        animator.SetFloat(
            "Speed",
            speed
        );
    }


    private void Jump()
    {
        // Reset vận tốc Y trước khi nhảy
        Vector3 velocity = rb.velocity;
        velocity.y = 0f;
        rb.velocity = velocity;

        // Animation Jump
        animator.SetTrigger("Jump");

        // Nhảy
        rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );

        isGrounded = false;
    }


    private void ApplyFallGravity()
    {
        if (rb.velocity.y < 0f)
        {
            rb.velocity +=
                Vector3.up *
                Physics.gravity.y *
                (fallMultiplier - 1f) *
                Time.fixedDeltaTime;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}