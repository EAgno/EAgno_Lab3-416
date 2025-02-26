using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int jumpCount = 0;
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private bool isOnGround;

    public float dashMultiplier = 3f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyGravity();
    }

    void Update()
    {
        HandleJump();
        HandleDash();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * moveX + forward * moveZ).normalized;

        float currentSpeed = moveSpeed;

        if (isDashing)
        {
            currentSpeed *= dashMultiplier;
        }

        Vector3 velocity = moveDirection * currentSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    void HandleDash()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpCount++;
        }
    }

    void ApplyGravity()
    {
        if (!isOnGround)
        {
            rb.linearVelocity += new Vector3(0, gravity * Time.fixedDeltaTime, 0);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isOnGround = true;
        jumpCount = 0;
    }

    void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }
}
