using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Dodge Roll Settings")]
    public float rollSpeed = 10f;        // Speed during roll
    public float rollDuration = 1f;    // How long roll lasts
    public float rollCooldown = 1f;    // Time before next roll allowed
    [HideInInspector]public bool isRolling = false;
    private bool canRoll = true;

    [Header("Attack Settings")]
    public Animator animator;        // The Animator controlling attack animation
    public Collider clawCollider;    // The collider on the claws (set as Trigger)
    public float attackDuration = 0.4f; // How long the attack lasts
    public float attackCooldown = 0.8f; // Time between attacks

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canAttack = true;
    [HideInInspector] public bool isAttacking = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();

        // Make sure claw collider starts disabled so it doesn't hit enemies passively
        if (clawCollider != null)
            clawCollider.enabled = false;
    }

    private void Update()
    {
        // Skip input if attacking or rolling
        if (isAttacking || isRolling)
            return;

        // ------------------ MOVEMENT ------------------
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // Keeps player grounded

        // Movement Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Only move if input exists
        Vector3 move = transform.right * x + transform.forward * z;
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        if (move.magnitude > 0.01f)
            controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ------------------ DODGE ROLL ------------------
        if (Input.GetKeyDown(KeyCode.LeftControl) && canRoll && isGrounded)
        {
            // Only roll if player presses a direction
            if (move.magnitude > 0.01f)
            {
                StartCoroutine(DodgeRoll(move.normalized));
            }
        }

        // ------------------ ATTACK ------------------
        if (Input.GetMouseButtonDown(0) && canAttack)
            StartCoroutine(Attack());
    }


    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        // Play animation
        if (animator != null)
            animator.SetTrigger("Attack");

        // Enable claw collider to detect hits
        if (clawCollider != null)
            clawCollider.enabled = true;

        // Wait for attack animation hit duration
        yield return new WaitForSeconds(attackDuration);

        // Disable claw collider again
        if (clawCollider != null)
            clawCollider.enabled = false;

        // End of attack animation
        isAttacking = false;

        // Wait for cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private IEnumerator DodgeRoll(Vector3 rollDir)
    {
        isRolling = true;
        canRoll = false;

        // Determine rotation axis
        Vector3 rotationAxis = Vector3.zero;
        float rotationAngle = 360f; // one full flip

        float forwardDot = Vector3.Dot(transform.forward, rollDir.normalized);
        float rightDot = Vector3.Dot(transform.right, rollDir.normalized);

        if (Mathf.Abs(forwardDot) > Mathf.Abs(rightDot))
        {
            // Forward/backward roll X axis
            rotationAxis = Vector3.right;
            if (forwardDot < 0) rotationAngle = -rotationAngle; // backward flip
        }
        else
        {
            // Left/right roll Z axis
            rotationAxis = Vector3.forward;
            if (rightDot > 0) rotationAngle = -rotationAngle; // right flip
            else rotationAngle = rotationAngle;               // left flip
        }

        // Trigger in-place roll animation
        if (animator != null)
            animator.SetTrigger("Roll");

        float elapsed = 0f;
        while (elapsed < rollDuration)
        {
            // Apply rotation over time
            float deltaAngle = (rotationAngle / rollDuration) * Time.deltaTime;
            transform.Rotate(rotationAxis, deltaAngle, Space.Self);

            controller.Move(rollDir.normalized * rollSpeed * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Player took {amount} damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        Debug.Log("died!");
    }
}
