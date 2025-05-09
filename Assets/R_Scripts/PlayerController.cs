using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public Animator animator;
    public Transform playerModel;
    public Shooter shooter;
    public AudioClip shootSFX;
    public AudioClip slashSFX;
    [SerializeField] private float raycastRange;

    private PlayerInputActions inputActions;
    private Vector2 movementInput;
    private Rigidbody rb;

    private const string IdleTrigger = "Idle";
    private const string ShootTrigger = "Shoot";
    private const string DefendTrigger = "Defend";
    private const string DeadTrigger = "Dead";

    private bool isAlive = true;
    private bool isShooting = false;
    private bool isDefending = false;
    private bool canMove = true;
    private bool isRunning = false;

    private float shootCooldownTimer = 0f;
    private float defendCooldownTimer = 0f;
    private float animationCooldown = 0.2f;

    private bool isGrounded;

    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpForce = 10f; // You can tweak this value in the Inspector



    void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Shoot.performed += ctx => StartShoot();
        inputActions.Player.Shoot.canceled += ctx => EndShoot();

        inputActions.Player.Defend.performed += ctx => StartDefend();
        inputActions.Player.Defend.canceled += ctx => EndDefend();
        inputActions.Player.Jump.performed += ctx => OnJump(ctx);
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isAlive) return;

        shootCooldownTimer -= Time.deltaTime;
        defendCooldownTimer -= Time.deltaTime;

        // Handle movement
        movementInput = canMove ? inputActions.Player.Move.ReadValue<Vector2>() : Vector2.zero;
        Vector3 moveDir = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        if (moveDir != Vector3.zero)
        {
            playerModel.rotation = Quaternion.LookRotation(moveDir);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (canMove && !isShooting && !isDefending)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastRange) && hit.transform.tag != "Portal")
            {
                Debug.Log($"{hit.transform.name}");
            }
            else
            {
                Vector3 newPosition = transform.position + moveDir * moveSpeed * Time.deltaTime;
                rb.MovePosition(newPosition);
            }
        }

        animator.SetBool("IsRunning", isRunning);

        if (!isRunning && !isShooting && !isDefending)
        {
            animator.SetTrigger(IdleTrigger);
        }

        Debug.Log($"Grounded: {isGrounded}, Y: {transform.position.y}, YVelocity: {rb.velocity.y}");

        Debug.Log("Rigidbody Velocity: " + rb.velocity);

        isGrounded = CheckIfGrounded(); // implement this

        animator.SetFloat("YVelocity", rb.velocity.y);
        Debug.Log("YVelocity: " + rb.velocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }



    void OnJump(InputAction.CallbackContext context)
    {
        // Log that jump input is triggered
        Debug.Log("Jump Input Triggered!");

        // Check if the player is grounded
        if (isGrounded)
        {
            // Log when the jump actually happens
            Debug.Log("Jumping");

            // Apply the jump force
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset Y velocity before applying jump force to avoid stack-up
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply impulse force to jump

            // Trigger the jump animation
            animator.SetTrigger("JumpTrigger");
        }
        else
        {
            Debug.Log("Not grounded, cannot jump");
        }
    }


    private bool CheckIfGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float checkDistance = groundCheckDistance;

        Ray ray = new Ray(origin, Vector3.down);
        bool grounded = Physics.Raycast(ray, out RaycastHit hit, checkDistance, groundMask);

        // Visual Ray
        Debug.DrawRay(origin, Vector3.down * checkDistance, grounded ? Color.green : Color.red);

        // Detailed Debug Info
        if (grounded)
        {
            Debug.Log($"[Grounded ] Hit '{hit.collider.name}' at distance {hit.distance}. Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
        }
        else
        {
            Debug.Log($"[Grounded ] No ground hit. Origin: {origin}, Distance: {checkDistance}, Ground Mask Value: {groundMask.value}");

            // Try a default LayerMask (just for testing)
            if (Physics.Raycast(ray, out RaycastHit fallbackHit, checkDistance))
            {
                Debug.Log($"[Fallback HIT] Without layer mask: '{fallbackHit.collider.name}', Layer: {LayerMask.LayerToName(fallbackHit.collider.gameObject.layer)}");
            }
            else
            {
                Debug.Log("[Fallback ] Still no hit without layer mask.");
            }
        }

        return grounded;
    }


    private void StartShoot()
    {
        if (!isShooting && shootCooldownTimer <= 0f && isAlive)
        {
            shooter.Shoot();
            SoundManager.instance.playSoundEffect(shootSFX);
            animator.SetTrigger(ShootTrigger);
            isShooting = true;
            canMove = false;
            shootCooldownTimer = animationCooldown;
        }
    }

    private void EndShoot()
    {
        if (isShooting)
        {
            animator.ResetTrigger(ShootTrigger);
            isShooting = false;
            canMove = true;
        }
    }

    private void StartDefend()
    {
        if (!isDefending && defendCooldownTimer <= 0f && isAlive)
        {
            SoundManager.instance.playSoundEffect(slashSFX);
            animator.SetTrigger(DefendTrigger);
            isDefending = true;
            canMove = false;
            defendCooldownTimer = 1f;
        }
    }

    private void EndDefend()
    {
        if (isDefending)
        {
            animator.ResetTrigger(DefendTrigger);
            isDefending = false;
            canMove = true;
        }
    }

    public void onDeadPlayer()
    {
        isAlive = false;
        animator.SetTrigger(DeadTrigger);
    }
}
