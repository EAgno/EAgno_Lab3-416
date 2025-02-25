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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // In case camera not found
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        ApplyGravity();
    }

    void Update()
    {
        HandleJump();
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

        // Direction based on camera orientation
        Vector3 moveDirection = (right * moveX + forward * moveZ).normalized;

        // Apply velocity
        Vector3 velocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
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
