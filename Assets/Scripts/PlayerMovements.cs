using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    [Header("Speed of the player")]
    [SerializeField] float camSpeed;
    [SerializeField] float movSpeed = 5f;
    [SerializeField] float runMultiplier = 1.8f;
    [SerializeField] float jumpForce = 5f;

    [Header("Références physiques")]
    [SerializeField] Collider playerCollider;
    [SerializeField] Rigidbody playerRb;

    [Header("Course")]
    [SerializeField] bool canRun = true;
    [SerializeField] bool runToggle = false;

    [Header("Détection du sol")]
    [SerializeField] LayerMask groundLayer = ~0;
    [SerializeField] float groundCheckDistance = 0.15f;

    [Header("Input Actions (Player map)")]
    [SerializeField] InputActionReference forward;
    [SerializeField] InputActionReference backward;
    [SerializeField] InputActionReference left;
    [SerializeField] InputActionReference right;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;

    bool isRunning;
    bool jumpQueued;
    Vector3 moveInput;

    public bool IsRunning => isRunning;

    void OnEnable()
    {
        forward.action.Enable();
        backward.action.Enable();
        left.action.Enable();
        right.action.Enable();
        jump.action.Enable();
        run.action.Enable();

        jump.action.performed += OnJump;
        run.action.performed += OnRunPerformed;
        run.action.canceled += OnRunCanceled;
    }

    void OnDisable()
    {
        jump.action.performed -= OnJump;
        run.action.performed -= OnRunPerformed;
        run.action.canceled -= OnRunCanceled;

        forward.action.Disable();
        backward.action.Disable();
        left.action.Disable();
        right.action.Disable();
        jump.action.Disable();
        run.action.Disable();
    }

    void Update()
    {
        float z = (forward.action.IsPressed() ? 1f : 0f) - (backward.action.IsPressed() ? 1f : 0f);
        float x = (right.action.IsPressed() ? 1f : 0f) - (left.action.IsPressed() ? 1f : 0f);
        moveInput = new Vector3(x, 0f, z).normalized;

        if (!canRun) isRunning = false;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
            jumpQueued = true;
    }

    void OnRunPerformed(InputAction.CallbackContext ctx)
    {
        if (!canRun) return;

        if (runToggle) isRunning = !isRunning;
        else isRunning = true;
    }

    void OnRunCanceled(InputAction.CallbackContext ctx)
    {
        if (!runToggle) isRunning = false;
    }

    void FixedUpdate()
    {
        float speed = movSpeed * (isRunning && canRun ? runMultiplier : 1f);

        Vector3 velocity = moveInput * speed;
        velocity.y = playerRb.linearVelocity.y;
        playerRb.linearVelocity = velocity;

        if (jumpQueued)
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpQueued = false;
        }
    }

    bool IsGrounded()
    {
        Vector3 origin = playerCollider.bounds.center;
        float dist = playerCollider.bounds.extents.y + groundCheckDistance;
        return Physics.Raycast(origin, Vector3.down, dist, groundLayer);
    }
}