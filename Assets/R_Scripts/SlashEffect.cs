using Unity.VisualScripting;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public GameObject slashParticlePrefab; // Reference to the slash particle prefab
    public Transform slashPointLeft; // Left slash point transform
    public float slashDuration = 2.0f; // Duration of the slash effect before it vanishes
    public float defendCooldownTime = 1f; // Cooldown time for defending

    private Animator animator; // Reference to the Animator component
    private float defendCooldownTimer = 0f; // Timer to track the defend cooldown

    void Start()
    {
        // Get the Animator component attached to the parent GameObject
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle defend cooldown timer
        if (defendCooldownTimer > 0)
        {
            defendCooldownTimer -= Time.deltaTime;
        }

        // Check for right mouse button press and defend cooldown
        if (Input.GetMouseButtonDown(1) && defendCooldownTimer <= 0)
        {
            // Trigger defend animation
            animator.SetTrigger("Defend");

            // Instantiate slash particle effect at left slash point
            GameObject slashParticle = Instantiate(slashParticlePrefab, slashPointLeft.position, Quaternion.identity);
            Destroy(slashParticle, slashDuration);

            // Start defend cooldown
            defendCooldownTimer = defendCooldownTime;
        }
    }
}
