using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public float rollDuration = 0.3f;    // How long roll lasts
    public float rollCooldown = 2.5f;    // Time before next roll allowed
    [HideInInspector] public bool isRolling = false;
    private bool canRoll = true;

    [Header("Attack Settings")]
    public Animator animator;        // The Animator controlling attack animation
    public Collider clawCollider;    // The collider on the claws (set as Trigger)
    public float attackDuration = 0.5f; // How long the attack lasts
    public float attackCooldown = 0f; // Time between attacks

    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;
    public int goobletCount;
    public List<GameObject> healthUI = new List<GameObject>();

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canAttack = true;
    [HideInInspector] public bool isAttacking = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();


        int bonus = 0;//this is the extrahealth from upgrades
        if (GameManager.Instance != null)
        {
            bonus = GameManager.Instance.extraHealth;
        }

        maxHealth += bonus;
        Debug.Log(maxHealth);
        currentHealth = maxHealth;

        // Make sure claw collider starts disabled so it doesn't hit enemies passively
        if (clawCollider != null)
            clawCollider.enabled = false;
        InitializeHeartsUI();
    }

    private void Update()
    {
        // Skip input if attacking or rolling
        if (isRolling)
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
    IEnumerator DodgeRoll(Vector3 rollDir)
    {
        isRolling = true;
        canRoll = false;

        float time = 0f;

        while (time < rollDuration)
        {
            // Fast movement burst
            controller.Move(rollDir.normalized * rollSpeed * Time.deltaTime);

            time += Time.deltaTime;
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
        {
            Die();
        }
        if (currentHealth > 0 && currentHealth <= maxHealth)
        {
            for (int i = 0; i < healthUI.Count; i++) 
            {

                healthUI[i].SetActive(i < currentHealth);
            }
            
            //healthUI[currentHealth].SetActive(false);

        }
    }
    private void Die()
    {
        SceneManager.LoadScene("Upgrades_Scene");
        Debug.Log("died!");
    }

    private void InitializeHeartsUI()
    {
        for (int i = 0; i < healthUI.Count; i++)
        {
            healthUI[i].SetActive(i < maxHealth);
        }
    }
}
