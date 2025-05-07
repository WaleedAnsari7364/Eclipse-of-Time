using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f; // Adjust this to control the movement speed
    public Animator animator; // Reference to the Animator component
    public Transform playerModel; // Reference to the player model (to rotate)
    public Shooter shooter; // Reference to the Shooter component

    // Animation trigger names
    private const string IdleTrigger = "Idle";
    private const string ShootTrigger = "Shoot";
    private const string DefendTrigger = "Defend";
    private const string DeadTrigger = "Dead"; // New trigger for dead animation

    private bool isAlive = true; // Flag to track if the player is alive
    private bool isShooting = false; // Flag to track if shooting animation is playing
    private bool isDefending = false; // Flag to track if defending animation is playing
    private bool isRunning = false; // Flag to track if the player is running
    private bool canMove = true; // Flag to track if the player can move
    private float animationCooldown = 0.2f; // Cooldown time for animations
    private float shootCooldownTimer = 0f; // Timer to track the shoot cooldown
    private float defendCooldownTimer = 0f; // Timer to track the defend cooldown

    private Rigidbody rb; // Reference to the Rigidbody component


    public AudioClip shootSFX;
    public AudioClip slashSFX;
    [SerializeField] float raycastRange;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isAlive)
            return; // Do nothing if the player is dead

        // Input for movement
        float horizontalInput = canMove ? Input.GetAxis("Horizontal") : 0f;
        float verticalInput = canMove ? Input.GetAxis("Vertical") : 0f;

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Rotate the player model to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            playerModel.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Move the player using Rigidbody's MovePosition method
        if (canMove && !isShooting && !isDefending)
        {

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastRange) && hit.transform.tag!="Portal")
			{
                Debug.Log($"{hit.transform.name}");
            }
            
			else
			{
                Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
                rb.MovePosition(newPosition);
                // Update running state
                isRunning = moveDirection != Vector3.zero;
                // Set animation parameters
                animator.SetBool("IsRunning", isRunning);
            }

        }
        else
        {
            // Set back to idle animation if shooting or defending
            animator.SetTrigger(IdleTrigger);
        }

        // Handle cooldown timer for shooting
        if (shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.deltaTime;
        }

        // Handle cooldown timer for defending
        if (defendCooldownTimer > 0)
        {
            defendCooldownTimer -= Time.deltaTime;
        }

        // Handle shoot animation
        if (Input.GetMouseButtonDown(0) && !isShooting && shootCooldownTimer <= 0) // Left click
        {
            shooter.Shoot();
            SoundManager.instance.playSoundEffect(shootSFX);

            // Set shooting flag and cooldown timer
            animator.SetTrigger(ShootTrigger);
            isShooting = true;
            canMove = false; // Disable movement
            shootCooldownTimer = animationCooldown; // Start shoot cooldown timer
        }
        else if (Input.GetMouseButtonUp(0)) // Release left click
        {
            animator.ResetTrigger(ShootTrigger);
            isShooting = false;
            canMove = true; // Enable movement
        }

        // Handle defend animation
        if (Input.GetMouseButtonDown(1) && !isDefending && defendCooldownTimer <= 0) // Right click
        {
            SoundManager.instance.playSoundEffect(slashSFX);
            animator.SetTrigger(DefendTrigger);
            isDefending = true;
            isShooting = false; // Reset shooting flag
            canMove = false; // Disable movement
            defendCooldownTimer = 1f; // Start defend cooldown timer (0.5 seconds)
        }
        else if (Input.GetMouseButtonUp(1)) // Release right click
        {
            animator.ResetTrigger(DefendTrigger);
            isDefending = false;
            canMove = true; // Enable movement
        }

        // Set back to idle animation when not clicking left or right click
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !isRunning)
        {
            animator.SetTrigger(IdleTrigger);
        }
    }

    public void onDeadPlayer()
    {
        isAlive = false; // Mark the player as dead
        animator.SetTrigger(DeadTrigger); // Play the dead animation
    }
}
